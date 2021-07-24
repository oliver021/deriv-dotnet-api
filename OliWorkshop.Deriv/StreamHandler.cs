using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OliWorkshop.Deriv.ApiResponse;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{
    /// <summary>
    /// The stream handler represent a class with parameters and data with control and manage
    /// a stream form websocket api connections
    /// </summary>
    public class StreamHandler<TStream>
        where TStream : IHasSubscription
    {
        /// <summary>
        /// This property indicate if the stream was forget
        /// </summary>
        public bool IsFotget { get; private set; }

        // these variables is basic reference of all dependecies
        private WebSocketStream webSocketStream;
        private readonly long track;
        private Channel<string> stream;
        private CancellationTokenSource token;
        private CancellationToken cancellation;

        // internal resource
        private SemaphoreSlim slim = new SemaphoreSlim(1,1);

        /// <summary>
        /// This is a internal construct because this class can be use public but
        /// only can instance from other public method of this project
        /// </summary>
        /// <param name="webSocketStream"></param>
        /// <param name="track"></param>
        /// <param name="stream"></param>
        /// <param name="cancellation"></param>
        internal StreamHandler(WebSocketStream webSocketStream, long track, Channel<string> stream)
        {
            this.webSocketStream = webSocketStream;
            this.track = track;
            this.stream = stream;
            token = new CancellationTokenSource();
            this.cancellation = token.Token;
        }

        /// <summary>
        /// This method allow execute a method or code that make your custon logic
        /// or process to handle stream data
        /// </summary>
        /// <param name="handler">
        /// This action is executed every time between a new Stream update message
        /// </param>
        public void Listen(Action<TStream, TStream> handler)
        {
            // put in background this function
            ThreadPool.QueueUserWorkItem(delegate {
                
                TStream last = default;

                // loop to track the response
                while (!cancellation.IsCancellationRequested && !stream.Reader.Completion.IsCompleted)
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
                        /// invoke the callable argument
                        /// pass old value and new value
                        handler.Invoke(last, last=JsonConvert.DeserializeObject<TStream>(response));
                    }

                    // forget the subscription
                    ForgetStream(last.Subscription).Wait();
                }
            });
        }

        /// <summary>
        /// This method return a simple async enumerable
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<TStream> GetStreamEnumerable()
        {
            TStream last = default;

            // loop to track the response
            while (!cancellation.IsCancellationRequested && !stream.Reader.Completion.IsCompleted)
            {
                string response = await stream.Reader.ReadAsync();

                // check if is subscriptions
                if (JToken.Parse(response).SelectToken("req_id").ToObject<long>().Equals(track))
                {
                    yield return last = JsonConvert.DeserializeObject<TStream>(response);
                }
            }

            // forget the subscription
            await ForgetStream(last.Subscription);
        }

        /// <summary>
        /// Forget the subscription of this stream to free resources
        /// </summary>
        /// <returns></returns>
        public void Forget()
        {
            // cancell process by token cancellation
            token.Cancel();

            // stop the channel propagation
            stream.Writer.Complete();

            // free resources
            token.Dispose();
        }

        /// <summary>
        /// The forget stream is a internal helper to use in several process that listen the stream
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        private async Task ForgetStream(SubscriptionInformation subscription)
        {
            await slim.WaitAsync();

            // only if the stream if not forget
            if (IsFotget is false)
            {
                // send request
                bool result = await webSocketStream.Forget(subscription.Id);

                // cjeck result
                if (result) {
                    // indicate that stream is forget
                    IsFotget = true;
                }
            }

            // free block
            slim.Release();
        }
    }
}
