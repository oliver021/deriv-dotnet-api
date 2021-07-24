using Newtonsoft.Json;
using OliWorkshop.Deriv.ApiRequest;
using OliWorkshop.Deriv.ApiRequests;
using OliWorkshop.Deriv.ApiResponse;
using OliWorkshop.Deriv.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{
    /// <summary>
    /// Clase que ofrece un API unificada para comunicarse con binary websocket api
    /// </summary>
    public class DerivApiService
    {
        
        public DerivApiService(WebSocketStream Stream)
        {
           _ws = Stream ?? throw new ArgumentNullException(nameof(Stream));
        }

        /// <summary>
        /// Indica si el usuario ha sido autorizado
        /// </summary>
        public bool IsAuthorize { get; set; }

        /// <summary>
        /// Datos del ultimo usuario autorizado
        /// </summary>
        public Authorize UserAuthorized { get; private set; }

        /// <summary>
        /// Socket necesario para el envio y recepcion de informacion
        /// </summary>
        public WebSocketStream _ws { get; }

        /// <summary>
        /// The subscriptions avaliable to use, manage and storage in this service
        /// </summary>
        internal Dictionary<string, SubscriptionHandler> _subscriptions = new Dictionary<string, SubscriptionHandler>();

        /// <summary>
        /// Statement reatrives data from deriv
        /// </summary>
        /// <param name="type"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Task<StatementResponse> GetStatement(DateTime from, DateTime to, long? limit = null, long? offset = null, ActionType? type = null, bool description = true)
        {
            var statement = new StatementRequest();
            statement.ActionType = type;
            statement.Limit = limit;
            statement.Offset = offset;
            statement.DateFrom = new DateTimeOffset(from).ToUnixTimeSeconds();
            statement.DateFrom = new DateTimeOffset(to).ToUnixTimeSeconds();
            statement.Description = description ? 1 : 0;

            return _ws.QueryAsync<StatementRequest, StatementResponse>(statement, StatementConverter.Settings);
        }

        /// <summary>
        /// Statement reatrives data from deriv
        /// </summary>
        /// <param name="type"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Task<StatementResponse> GetStatement(long page = 1, long size = 50, ActionType? type = null, bool description = true)
        {
            if (page < 1)
            {
                // the page not be less than 1
                throw new InvalidOperationException($"Invalid page {page}, the page number should be greather that 0");
            }

            // create request object to fill with parameters
            var statement = new StatementRequest();

            statement.ActionType = type;
            statement.Limit = size;
            statement.Offset = ((page-1) * size);
            statement.Description = description ? 1 : 0;

            return _ws.QueryAsync<StatementRequest, StatementResponse>(statement, StatementConverter.Settings);
        }

        /// <summary>
        /// Statement reatrives data from deriv
        /// </summary>
        /// <param name="type"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Task<StatementResponse> GetStatement(DateTime from, DateTime to, ActionType? type, bool description = true)
        {
            var statement = new StatementRequest();
            statement.ActionType = type;
            statement.DateFrom = new DateTimeOffset(from).ToUnixTimeSeconds();
            statement.DateFrom = new DateTimeOffset(to).ToUnixTimeSeconds();
            statement.Description = description ? 1 : 0;

            return _ws.QueryAsync<StatementRequest, StatementResponse>(statement, StatementConverter.Settings);
        }

        /// <summary>
        /// Statement reatrives data from deriv
        /// </summary>
        /// <param name="type"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<ProfitTable> GetProfitTable(long page = 1, long size = 50, ContractType[] type = null, bool description = true, Sort sort = Sort.Asc)
        {
            if (page < 1)
            {
                // the page not be less than 1
                throw new InvalidOperationException($"Invalid page {page}, the page number should be greather that 0");
            }

            // create request object to fill with parameters
            var request = new ProfitTableRequest();

            // set the parameters
            request.ContractType = type;
            request.Limit = size;
            request.Offset = ((page - 1) * size);
            request.Description = description ? 1 : 0;
            request.Sort = sort;

            var response =  await _ws.QueryAsync<ProfitTableRequest, ProfitTableResponse>(request);

            // return a target object
            return response.ProfitTable;
        }

        /// <summary>
        /// The profit table query builder
        /// This method allow create a query object to poll profits table
        /// </summary>
        /// <returns></returns>
        public QueryProfits QueryProfitTable()
        {
            // this object allow create a custom query to poll profit table
            return new QueryProfits(_ws);
        }

        /// <summary>
        /// Create a object iterator to poll history data
        /// </summary>
        /// <param name="market"></param>
        /// <param name="timePage"></param>
        /// <returns></returns>
        public HistoryQuery QueryHistory(string market, int timePage = 3600)
        {
            // return the object that allow to poll hsitory data
            // this method build an instance with the preset parameter and service
            // of the data streams
            return new HistoryQuery(_ws, market, timePage);
        }

        /// <summary>
        /// Create a object iterator to poll history data
        /// </summary>
        /// <param name="market"></param>
        /// <param name="timePage"></param>
        /// <returns></returns>
        public CandlesHistoryQuery QueryCandles(string market)
        {
             // return the object that allow to poll hsitory data
            // this method build an instance with the preset parameter and service
            // of the data streams
            return new CandlesHistoryQuery(_ws, market);
        }

        /// <summary>
        /// Make a query of the history data from time short period no more than 5000 ticks
        /// </summary>
        /// <param name="market"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public async Task<History> QuickHistory(string market, long? period)
        {
            var result = await _ws.QueryAsync<TicksHistoryRequest, TicksHistoryResponse>(new TicksHistoryRequest { 
                End = "latest",
                Start = period,
                Style = Style.Ticks,
                TicksHistory = market
            }, TickHistoryRequestConverter.Settings);

            // return value
            return result.History;
        }

        /// <summary>
        /// Hace una consulta al API de los datos historicos
        /// </summary>
        /// <param name="market"></param>
        /// <param name="timePage"></param>
        /// <returns></returns>
        public async Task<History> QuickHistory(string market, long start, long end)
        {
            var result = await _ws.QueryAsync<TicksHistoryRequest, TicksHistoryResponse>(new TicksHistoryRequest
            {
                End = end.ToString(),
                Start = start,
                Style = Style.Ticks,
                TicksHistory = market
            }, TickHistoryRequestConverter.Settings);

            // return value
            return result.History;
        }

        /// <summary>
        /// Hace una consulta al API de los datos historicos creando sessiones de tiempo
        /// </summary>
        /// <param name="market"></param>
        /// <param name="timePage"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<History> QueryHistory(string market, long start, long period, int count = 10)
        {
            for (int i = 0; i < count; i++)
            {
                var result = await _ws.QueryAsync<TicksHistoryRequest, TicksHistoryResponse>(new TicksHistoryRequest
                {
                    End = (start - (i * period)).ToString(),
                    Start = start - ((i + 1) * period),
                    Style = Style.Ticks,
                    TicksHistory = market
                }, TickHistoryRequestConverter.Settings);

                // return value
                yield return result.History;
            }
        }

        /// <summary>
        ///  Make a query with avaliable symbols in the Deriv trading Platform
        /// </summary>
        /// <param name="type"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<ActiveSymbol[]> GetSymbols(ActiveSymbols type = ActiveSymbols.Full, LandingCompany? company = null)
        {
            // set parameters of request to poll about active symbols
            var response = await _ws.QueryAsync<ActiveSymbolsRequest, ActiveSymbolResponse>(new ActiveSymbolsRequest { 
                ActiveSymbols = type,
                LandingCompany = company
            });

            // return a list of the active symbols
            return response.ActiveSymbols;
        }

        /// <summary>
        /// Hace una consulta a los limites de una cuenta
        /// </summary>
        /// <returns></returns>
        public Task<History> GetLimits()
        {
            return null;
        }

        /// <summary>
        /// Make a query of the avaliable balance in an account
        /// </summary>
        /// <returns></returns>
        public async Task<Balance> GetBalance(string account = "current")
        {
            if (IsAuthorize is false)
            {
                throw new InvalidOperationException("You must be autheticated to call this method");
            }

            /// send a query
            var response = await _ws.QueryAsync<BalanceRequest, BalanceResponse>(new BalanceRequest { 
                Account = account
            });

            return response.Balance;
        }

        /// <summary>
        /// Subscription is balance changes to listen changes about account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<Balance> SubscribeBalanceChanges(string account = "current")
        {
            if (IsAuthorize is false)
            {
                throw new InvalidOperationException("You must be autheticated to call this method");
            }

            /// send a query with a subscription
            var response = await _ws.QueryAsync<BalanceRequest, BalanceResponse>(new BalanceRequest
            {
                Account = account,
                Subscribe = 1
            });

            return response.Balance;
        }

        /// <summary>
        /// Reatrive all contracts avaliable for a specific instrument by a symbol
        /// </summary>
        /// <returns></returns>
        public async Task<ContractsList> GetContracts(string symbol, string concurrency = "current", LandingCompany? company = null)
        {
            var response = await _ws.QueryAsync<ContractForRequest, ContractForResponse>(new ContractForRequest {
                // set parameters
                ContractsFor = symbol,
                Currency = (concurrency == "curent") ? UserAuthorized.Currency : concurrency,
                LandingCompany = company
            },
            ContractRequestForConverter.Settings, ContractResponseForConverter.Settings);
            return response.ContractsFor;
        }

        /// <summary>
        /// Sign in by token
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Authorize(string token)
        {
            var response = await _ws.QueryAsync<AuthorizeRequest, AuthorizeResponse>(
               new AuthorizeRequest
               {
                   Authorize = token,
               });


            bool result = response.Authorize != null;

            // si la autenticacion tuvo exito entonces se procede
            // a almacenar los datos de usuario comoe estado
            if (result)
            {
                UserAuthorized = response.Authorize;
            }
            return result;
        }

        /// <summary>
        /// Hace una consulta a la propuesta de un contrato
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProposalOpenContract> GetOpenProposal(long id)
        {
            var proposal = await _ws.QueryAsync<ProposalOpenRequest, ProposalOpenResponse>(new ProposalOpenRequest
            {
                ContractId = id,
                ProposalOpenContract = 1
            }, null, ProposalOpenConverter.Settings);

            return proposal.ProposalOpenContract;
        }

        /// <summary>
        /// Buy parameters by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<ContractHandler> BuyContract(string id, double price)
        {
            var response = await _ws.QueryAsync<BuyRequest, BuyResponse>(new BuyRequest { 
                Buy = id,
                Price = price
            }, SerializerOptions.BuySerializer);

            // devolver el objeto de la compra
            return new ContractHandler(response.Buy, _ws);
        }

        /// <summary>
        /// Buy a contract by parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<ContractHandler> BuyContract(DigitalOption option, long price)
        {
            var response = await _ws.QueryAsync<BuyRequest, BuyResponse>(new BuyRequest
            {
                Buy = "1",
                Price = price
            }, SerializerOptions.BuySerializer);

            // devolver el objeto de la compra
            return new ContractHandler(response.Buy, _ws);
        }

        /// Buy a contract by parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<Sell> SellContract(long id, long price)
        {
            var response = await _ws.QueryAsync<SellRequest, SellResponse>(new SellRequest
            {
                Sell = id,
                Price = price
            }, SerializerOptions.BuySerializer);

            // devolver el objeto de la compra
            return response.Sell;
        }

        /// <summary>
        /// Envia una propuesta de contrato para obtener
        /// los ultimos precios
        /// </summary>
        /// <returns></returns>
        public async Task<ProposalHandler> SendProposal(DConcurrency concurrency, DigitalOption option)
        {
            await Task.CompletedTask;
            return default;
        }

        /// <summary>
        /// Send a proposal by a contract parameters using the concurrency of current users
        /// this overload is short helper to skip set the concurrency
        /// </summary>
        /// <returns></returns>
        public async Task<ProposalHandler> SendProposal(DigitalOption option)
        {
            await Task.CompletedTask;
            return default;
        }

        /// <summary>
        /// Devuelve un enumerable asincrono con las actualizaciones 
        /// de los datos de cambios de precio en tiempo real
        /// </summary>
        /// <param name="market"></param>
        public IAsyncEnumerable<TicksResponse> GetInstrumentStream(string instrument)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new TicksRequestConverter() }
            };

            // create stream mapped from tick stream in 'binary ws api'
            return _ws.CreateStream<TicksRequest, TicksResponse>(new TicksRequest { Ticks = instrument }, settings);
        }

        /// <summary>
        /// Devuelve un enumerable asincrono con las actualizaciones 
        /// de los datos de cambios de precio en tiempo real
        /// </summary>
        /// <param name="market"></param>
        public IAsyncEnumerable<TimeTuple<double, long>> SubscribeForSymbol(string market)
        {
            return DerivGeneralHelpers.GetValueSubscription(_ws, market);
        }


        /// <summary>
        /// Devuelve un enumerable asincrono con las actualizaciones 
        /// de los datos de cambios de precio en tiempo real
        /// </summary>
        /// <param name="market"></param>
        public IAsyncEnumerable<TimeTuple<double, long>> ForgetSymbol(string market)
        {
            return DerivGeneralHelpers.GetValueSubscription(_ws, market);
        }
    }
}
