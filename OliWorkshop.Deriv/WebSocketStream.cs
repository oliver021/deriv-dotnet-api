using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{
    /// <summary>
    /// Servicio para solicitar stream y consultas por websockets
    /// </summary>
    public class WebSocketStream : WebSocketAbstractions
    {
        public WebSocketStream()
        {
        }

        public WebSocketStream(string uri, ILogger logger) : base(uri)
        {
            _logger = logger;
        }

        // basic contants
        private const string LossConnection = "The remote party closed the WebSocket connection without completing the close handshake.";

        /// <summary>
        /// referencia a un channel uqe actua como bus
        /// </summary>
        readonly ConcurrentDictionary<long, Channel<string>> streams = new ConcurrentDictionary<long, Channel<string>>();

        /// <summary>
        /// referencia de tareas pendientes
        /// </summary>
        readonly ConcurrentDictionary<long, Action<string>> requests = new ConcurrentDictionary<long, Action<string>>();

        /// <summary>
        /// Desconecta el websocket
        /// </summary>
        /// <returns></returns>
        public Task Disconnect()
        {
            _cancellationTokenSource.Cancel();
            return _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "request user", default);
        }

        /// <summary>
        /// begin litsen async
        /// </summary>
        /// <returns></returns>
        public async Task StartListenAsync()
        {
            // set buffer
            var buffer = new byte[ReceiveChunkSize];
            int counter;

        gotoConnect:

            // first conecting
            await ConnectAsync();

            counter = 1;

            // log info to connect
            _logger?.LogDebug("Socket conectado correctamente al servidor remoto");

            // attempt to recive message
            try
            {
                // flag to position
                loopToListen:
                while (_ws.State == WebSocketState.Open && !_cancellationToken.IsCancellationRequested)
                {
                    await DispatchMessage(buffer);
                }

                // close async connection
                if (_cancellationToken.IsCancellationRequested && _ws.State == WebSocketState.Open)
                {
                    _logger?.LogInformation("conexion cancelada");
                    // close by user
                    await _ws.CloseAsync(WebSocketCloseStatus.Empty, "close by user", default);
                }else if (!_cancellationToken.IsCancellationRequested)
                {
                    // switch ws states
                    if (_ws.State == WebSocketState.Connecting)
                    {
                        _logger?.LogWarning("El servidor esta reconectandose...");

                        // go to loop
                        goto loopToListen;
                    }else if (_ws.State == WebSocketState.Closed && _ws.State == WebSocketState.CloseReceived)
                    {
                        _logger?.LogError("Hubo una desconexion con el endpoint...");

                        // goto connect function
                        goto gotoConnect;
                    }
                    else
                    {
                        _logger?.LogError("Hubo un error al parecer, estado del socket: {0}", _ws.State.ToString());
                        _logger?.LogDebug("Estado del cierre: {0}", _ws.CloseStatus.ToString());
                        _logger?.LogDebug("Descripcion del estado del socket: {0}", _ws.CloseStatusDescription);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger?.LogInformation("terminate bu user request");
            }
            catch (WebSocketException wsErr) when(wsErr.Message == LossConnection)
            {
                // counter is increment
                if(counter < 32)
                    counter *= 2;

                _logger?.LogError($"Fallo la conexion de websocket. Reconectando en {counter} segundos...");
                
                // espera del error
                await Task.Delay(counter * 1000);
                
                goto gotoConnect;
            }
            catch (Exception err)
            {
                // show the error
                _logger?.LogError("Exception: {0}; Msg: {1}", err.GetType().Name, err.Message);
            }
            finally
            {
                _ws.Dispose();
            }
        }

        /// <summary>
        /// Mediante este metodo se enclapsura el codigo necesario para despachar un mensaje recivido
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private async Task DispatchMessage(byte[] buffer)
        {
            var stringResult = new StringBuilder();

            WebSocketReceiveResult result;
            do
            {
                result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), _cancellationToken);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await
                        _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    var str = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    stringResult.Append(str);
                }

            } while (!result.EndOfMessage && !_cancellationToken.IsCancellationRequested);

            // send to bus
            await DispatchMessage(stringResult.ToString());
        }

        /// <summary>
        /// Despacha un mnesjqe por medio de la conexion abierta
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private ValueTask DispatchMessage(string message)
        {
            long id = JToken.Parse(message).SelectToken("req_id").ToObject<long>();
            bool isStream = streams.ContainsKey(id);
            if (isStream)
            {
                if (streams.TryGetValue(id, out var current))
                {
                    return current.Writer.WriteAsync(message);
                }
                else
                {
                    throw new SocketStreamException("Stream Dispatcher not found");
                }
            }
            else
            {
                bool success = requests.TryGetValue(id, out Action<string> dispatcher);
                
                if (!success)
                {
                    throw new SocketStreamException("Request Dispatcher not found");
                }
                
                // ejecuta el handler message
                dispatcher.Invoke(message);
            }

            return new ValueTask(Task.CompletedTask);
        }

        /// <summary>
        /// field to generate numbers
        /// </summary>
        readonly Random randomGen = new Random();

        /// basic logger
        private ILogger _logger;

        /// <summary>
        /// Crea un flujo de actualizaciones para solicitudes de datos en tiempo real
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="settings"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TOutput> CreateStream<TInput, TOutput>(TInput input, JsonSerializerSettings settings = null, [EnumeratorCancellation] CancellationToken cancellation = default)
            where TInput : TrackObject
        {
            long track = randomGen.Next();
            input.ReqId = track;

            string request = JsonConvert.SerializeObject(input, settings);

            // send first
            await SendAsync(request);

            // store stream channel
            Channel<string> stream = Channel.CreateUnbounded<string>();

            // guard
            streams.TryAdd(track, stream);

            // loop to track the response
            while (!cancellation.IsCancellationRequested && !stream.Reader.Completion.IsCompleted)
            {
                bool success  = stream.Reader.TryRead(out string response);
                
                // check success
                if (!success)
                {
                    continue;
                }

                // check if is subscriptions
                if (JToken.Parse(response).SelectToken("req_id").ToObject<long>().Equals(track))
                {
                    yield return JsonConvert.DeserializeObject<TOutput>(response);
                }
            }
        }

        ///////////////////// TODO****
        /// <summary>
        /// Crea un flujo de actualizaciones para solicitudes de datos en tiempo real
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="settings"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task ListenStream<TInput, TOutput>(int hash, TInput input, Action<TOutput> callable, JsonSerializerSettings settings = null)
            where TInput : TrackObject
        {
            long track = randomGen.Next();
            input.ReqId = track;

            string request = JsonConvert.SerializeObject(input, settings);

            // send first
            await SendAsync(request);

            // store stream channel
            Channel<string> stream = Channel.CreateUnbounded<string>();

            // guard
            streams.TryAdd(track, stream);

            // loop to track the response
            while (!_cancellationToken.IsCancellationRequested && !stream.Reader.Completion.IsCompleted)
            {
                bool success = stream.Reader.TryRead(out string response);

                // check success
                if (!success)
                {
                    continue;
                }

                // check if is subscriptions
                if (JToken.Parse(response).SelectToken("req_id").ToObject<long>().Equals(track))
                {
                    callable.Invoke( JsonConvert.DeserializeObject<TOutput>(response) );
                }
            }
        }

        /// <summary>
        /// Crea un flujo de actualizaciones para solicitudes de datos en tiempo real
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="settings"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<TOutput> QueryAsync<TInput, TOutput>(TInput input, JsonSerializerSettings settings = null, JsonSerializerSettings responseSetting = null, CancellationToken cancellation = default)
            where TInput : TrackObject
        {
            long track = randomGen.Next();
            input.ReqId = track;

            string request = JsonConvert.SerializeObject(input, settings);

            // loop to track the response
            var src = new TaskCompletionSource<TOutput>(TaskCreationOptions.RunContinuationsAsynchronously);

            // add request handler to future execution
            // when the response is avalible
            requests.TryAdd(track, msg =>
            {
                // unserialized json source response
                TOutput responseObj = JsonConvert.DeserializeObject<TOutput>(msg, responseSetting);
                
                // dispatch completion
                src.SetResult(responseObj);

                // forget handler
                requests.TryRemove(track, out _);
            });

            // send first
            await SendAsync(request);

            /// finalmente se devuelve el resultado de la tarea
            return await src.Task;
        }
    }
}
