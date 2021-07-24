namespace OliWorkshop.Deriv
{
    using OliWorkshop.Deriv.ApiRequest;
    using OliWorkshop.Deriv.ApiResponse;
    using System.Threading.Tasks;

    /// <summary>
    /// This object help to create a query that allow to poll hsitory data from candles data model
    /// </summary>
    public class CandlesHistoryQuery
    {
        private WebSocketStream ws;
        private string market;
        private int _period = 0;
        private long _start;
        private long _end = 0;
        private bool _ensureStart = false;
        private long? _count = null;

        public CandlesHistoryQuery(WebSocketStream ws, string market)
        {
            this.ws = ws;
            this.market = market;
        }

        /// <summary>
        /// Set granulity parameter to set a size of a candle
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public CandlesHistoryQuery SetGranulity(int period)
        {
            _period = period;
            return this;
        }

        /// <summary>
        /// Limit the candles in the query
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public CandlesHistoryQuery Limit(int count)
        {
            _count = count;
            return this;
        }

        /// <summary>
        /// Set date paramaters to search in the determinate range of time 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public CandlesHistoryQuery From(long start, long end = 0)
        {
            _start = start;
            _end = end;
            return this;
        }

        /// <summary>
        /// If this method is called then the adjust time
        /// start is enable in the ticks hsitory reuqest
        /// </summary>
        /// <returns></returns>
        public CandlesHistoryQuery AdjustTimeStart()
        {
            _ensureStart = true;
            return this;
        }

        /// <summary>
        /// Execute a query with the parameters preset in this object
        /// </summary>
        /// <returns></returns>
        public async Task<Candle[]> RunQuery()
        {
            var response = await ws.QueryAsync<TicksHistoryRequest, TicksHistoryResponse>(new TicksHistoryRequest { 
                // set request parameters
                Count = _count,
                AdjustStartTime = _ensureStart ? 1: 0,
                End = _end == 0 ? "latest" : _end.ToString(),
                Start = _start,
                Granularity = _period,
                Style = Style.Candles

            }, TickHistoryRequestConverter.Settings, ConverterTickHistoryResponse.Settings);

            // return the candles objects
            return response.Candles;
        }
    }
}