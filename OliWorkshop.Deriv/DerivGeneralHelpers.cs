using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace RTForce.Topic.Deriv
{
    public static class DerivGeneralHelpers
    {
        /// <summary>
        /// Obtiene los valores para subscripcion
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /*public static IEnumerable<Tuple<string, IAsyncEnumerable<TickValue>>> GetValueSubscription(WebSocketStream ws, IEnumerable<string> enumerable)
        {
            return enumerable.Select(x => {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new TicksRequestConverter() }
                };

                // create stream mapped from tick stream in 'binary ws api'
                var stream = ws.CreateStream<TicksRequest, TicksResponse>(new TicksRequest { Ticks = x }, settings)
                .MapAsync(u => new TickValue(u.Tick.Quote.Value, u.Tick.Epoch.Value));

                return Tuple.Create(x, stream);
            });
        }*/

        /// <summary>
        /// Obtiene los valores para subscripcion
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /*public static  IAsyncEnumerable<TimeTuple<double, long>> GetValueSubscription(WebSocketStream ws, string symbol)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new TicksRequestConverter() }
            };

            // create stream mapped from tick stream in 'binary ws api'
            return ws.CreateStream<TicksRequest, TicksResponse>(new TicksRequest { Ticks = symbol }, settings)
            .MapAsync(u => new TimeTuple<double, long>(u.Tick.Epoch.Value, u.Tick.Quote.Value));
        }*/

    }
}
