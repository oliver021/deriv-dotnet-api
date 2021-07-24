using OliWorkshop.Deriv.ApiRequest;
using OliWorkshop.Deriv.ApiResponse;
using OliWorkshop.Topic.Deriv.ApiRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{
    /// <summary>
    /// History query is an object to poll history instrument values
    /// </summary>
    public class HistoryQuery
    {
        public WebSocketStream ws;
        
        public string market;

        public int lengthCount;

        public HistoryQuery(WebSocketStream stream, string market, int lengthCount)
        {
            this.ws = stream;
            this.market = market;
            this.lengthCount = lengthCount;
        }

        long lastTimeSpan = DateTimeOffset.Now.ToUnixTimeSeconds();
        int page = 1;

        /// <summary>
        /// The iteration poll data
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Tuple<double, long>>> NextAsync()
        {
            var query = await ws.QueryAsync<TicksHistoryRequest, TicksHistoryResponse>(new TicksHistoryRequest { 
                TicksHistory = this.market,
                Start = lastTimeSpan - (page * lengthCount),
                End = (lastTimeSpan - ((page-1) * lengthCount)).ToString(),
                Style = Style.Ticks
            }, TickHistoryRequestConverter.Settings);
            page++;
            return ToEnumerable(query);
        }

        /// <summary>
        /// Save a data in file cache
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        public async Task PutCache(string filename, int iterations)
        {
            var file = File.Create(filename);

            while (page < iterations)
            {
                await FetchData();
            }

            async Task FetchData()
            {
                var query = await ws.QueryAsync<TicksHistoryRequest, TicksHistoryResponse>(new TicksHistoryRequest
                {
                    TicksHistory = this.market,
                    Start = lastTimeSpan - (page * lengthCount),
                    End = (lastTimeSpan - ((page - 1) * lengthCount)).ToString(),
                    Style = Style.Ticks
                }, TickHistoryRequestConverter.Settings);
                page++;
                var result = query.History.Prices.Select(x => BitConverter.GetBytes(x)).SelectMany(x => x);
                await file.WriteAsync(result.ToArray());
            }

        }

        /// <summary>
        /// The iteration poll data with seek parameters
        /// </summary>
        /// <param name="seek"></param>
        /// <returns></returns>
        public Task<IEnumerable<Tuple<double, long>>> NextAsync(int seek)
        {
            // set new page position
            page += seek;

            if (page <= 0)
            {
                page = 1;
            }

            return NextAsync();
        }

        /// <summary>
        /// Simple helper
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private IEnumerable<Tuple<double, long>> ToEnumerable(TicksHistoryResponse query)
        {
            var values = query.History.Prices;
            var times = query.History.Times;
            var length = query.History.Times.Length;

            // iterator logic
            for (int i = 0; i < length; i++)
            {
                yield return Tuple.Create(values[i], times[i]);
            }
        }
    }
}