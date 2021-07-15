using Newtonsoft.Json;
using OliWorkshop.Deriv.ApiRequest;
using OliWorkshop.Deriv.ApiRequests;
using OliWorkshop.Deriv.ApiResponse;
using OliWorkshop.Deriv.Objects;
using OliWorkshop.Topic.Deriv.ApiRequest;
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
                throw new InvalidOperationException($"Invalid page {page}, the page number should be greather that 0");
            }

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
        public Task<StatementResponse> GetStatement(ActionType type, DateTime from, DateTime to, bool description = true)
        {
            var statement = new StatementRequest();
            statement.ActionType = type;
            statement.DateFrom = new DateTimeOffset(from).ToUnixTimeSeconds();
            statement.DateFrom = new DateTimeOffset(to).ToUnixTimeSeconds();
            statement.Description = description ? 1 : 0;

            return _ws.QueryAsync<StatementRequest, StatementResponse>(statement, StatementConverter.Settings);
        }

        /// <summary>
        /// Hace una consulta al API de los datos historicos
        /// </summary>
        /// <param name="market"></param>
        /// <param name="timePage"></param>
        /// <returns></returns>
        public HistoryQuery QueryHistory(string market, int timePage = 3600)
        {
            return new HistoryQuery(_ws, market, timePage);
        }

        /// <summary>
        /// Hace una consulta al API de los datos historicos
        /// </summary>
        /// <param name="market"></param>
        /// <param name="timePage"></param>
        /// <returns></returns>
        public async Task<History> QuickHistory(string market, long? timePage)
        {
            var result = await _ws.QueryAsync<TicksHistoryRequest, TicksHistotyResponse>(new TicksHistoryRequest { 
                End = "latest",
                Start = timePage,
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
            var result = await _ws.QueryAsync<TicksHistoryRequest, TicksHistotyResponse>(new TicksHistoryRequest
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
                var result = await _ws.QueryAsync<TicksHistoryRequest, TicksHistotyResponse>(new TicksHistoryRequest
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
        /// Hace una consulta de los instrumentos financieros disponibles
        /// </summary>
        /// <returns></returns>
        public Task<History> GetSymbols()
        {
            return null;
        }

        /// <summary>
        /// Hace una consulta a la tabla de ganancias
        /// </summary>
        /// <returns></returns>
        public Task<History> GetProfitTable()
        {
            return null;
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
        /// Hace una consulta a los limites de una cuenta
        /// </summary>
        /// <returns></returns>
        public Task<History> GetBalance()
        {
            return null;
        }

        /// <summary>
        /// Hace una consulta a los contratos disponibles para un
        /// instrumento en especifico
        /// </summary>
        /// <returns></returns>
        public Task<History> GetContracts(string symbol)
        {
            return null;
        }

        /// <summary>
        /// Hace una consulta a los limites de una cuenta
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
        /// Establece la moneda de la cuenta
        /// </summary>
        /// <returns></returns>
        public Task<bool> SetConcurrency(string concurrency)
        {
            return null;
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
        /// Compra un contrato basado en el id de una propuesta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<ContractHandler> BuyContract(long id, long price)
        {
            var response = await _ws.QueryAsync<BuyRequest, BuyResponse>(new BuyRequest { 
                Buy = id.ToString(),
                Price = price
            }, SerializerOptions.BuySerializer);

            // devolver el objeto de la compra
            return new ContractHandler(response.Buy, _ws);
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
