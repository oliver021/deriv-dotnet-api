using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{
    /// <summary>
    /// Base para crear clientes por websocket en dependencia de las necesidades
    /// de la una applicacion
    /// </summary>
    public class WebSocketAbstractions
    {
        /// <summary>
        /// Count bytes size to recive a message
        /// </summary>
        public const int ReceiveChunkSize = 1024 * 100;

        /// <summary>
        /// Count to chunck sizes
        /// </summary>
        public const int SendChunkSize = 1024;

        public WebSocketAbstractions()
        {
        }

        public WebSocketAbstractions(string uri)
        {
            Initialize(uri);
        }

        public void Initialize(string uri)
        {
            _uri = new Uri(uri);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            _ws = new ClientWebSocket();
            _ws.Options.KeepAliveInterval = TimeSpan.FromSeconds(20);
            _cancellationToken = _cancellationTokenSource.Token;
        }

        protected  ClientWebSocket _ws;
        protected  Uri _uri;
        protected  CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        protected CancellationToken _cancellationToken;

        /// <summary>
        /// Envia un mensaje emdiante la conexion abierta
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendAsync(string message)
        {
            while (_ws.State == WebSocketState.Connecting) { };
            if (_ws.State != WebSocketState.Open)
            {
                throw new WebSocketException("Connection is not open.");
            }

            var messageBuffer = Encoding.UTF8.GetBytes(message);
            var messagesCount = (int)Math.Ceiling((double)messageBuffer.Length / SendChunkSize);

            for (var i = 0; i < messagesCount; i++)
            {
                var offset = (SendChunkSize * i);
                var count = SendChunkSize;
                var lastMessage = ((i + 1) == messagesCount);

                if ((count * (i + 1)) > messageBuffer.Length)
                {
                    count = messageBuffer.Length - offset;
                }

                await _ws.SendAsync(new ArraySegment<byte>(messageBuffer, offset, count), WebSocketMessageType.Text, lastMessage, _cancellationToken);
            }
        }

        /// <summary>
        /// Comienza a abir la conexion por websockets
        /// </summary>
        /// <returns></returns>
        public Task ConnectAsync()
        {
            if (_ws.State == WebSocketState.Open || _cancellationToken.IsCancellationRequested)
                return Task.CompletedTask;
            return _ws.ConnectAsync(_uri, _cancellationToken);
        }
    }
}