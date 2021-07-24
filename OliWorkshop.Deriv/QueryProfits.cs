using OliWorkshop.Deriv.ApiRequest;
using OliWorkshop.Deriv.ApiResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{
    /// <summary>
    /// The basic object to create a customizable queries with fluent API
    /// </summary>
    public class QueryProfits
    {
        private WebSocketStream ws;

        // parameters
        private List<ContractType> _contracts;
        private long _page;
        private DateTime _fromDate;
        private DateTime _untilDate;

        /// <summary>
        /// requiere the websocket stream to continue
        /// </summary>
        /// <param name="ws"></param>
        public QueryProfits(WebSocketStream ws)
        {
            this.ws = ws;
            _contracts = new List<ContractType>();
        }

        /// <summary>
        /// The parameters to define a page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public QueryProfits Page(long page, long size = 50)
        {
            _page = page;
            return this;
        }

        /// <summary>
        /// Define the contract type to filter the table
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public QueryProfits AddFilter(ContractType contract)
        {
            _contracts.Add(contract);
            return this;
        }

        /// <summary>
        /// Define the contract type to filter the table
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public QueryProfits AddFilter(params ContractType[] types)
        {
            _contracts.AddRange(types);
            return this;
        }

        /// <summary>
        /// Define the date to filter a list of the table
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public QueryProfits From(DateTime from)
        {
            _fromDate = from;
            return this;
        }

        /// <summary>
        /// Define the date to filter a list of the table
        /// </summary>
        /// <param name="until"></param>
        /// <returns></returns>
        public QueryProfits Until(DateTime until)
        {
            _untilDate = until;
            return this;
        }

        public async Task<ProfitTable> RunQuery()
        {
            var request = BuildRequest();
            var response = await ws.QueryAsync<ProfitTableRequest, ProfitTableResponse>(request, ConverterProfitTable.Settings);
            return response.ProfitTable;
        }

        /// <summary>
        /// Build from parameters
        /// </summary>
        /// <returns></returns>
        private ProfitTableRequest BuildRequest()
        {
            throw new NotImplementedException();
        }
    }
}