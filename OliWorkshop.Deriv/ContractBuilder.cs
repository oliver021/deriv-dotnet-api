using OliWorkshop.Deriv.ApiRequest;
using OliWorkshop.Deriv.ApiResponse;
using OliWorkshop.Deriv.Objects;
using OliWorkshop.Topic.Deriv.ApiRequest;
using System;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{
    /// <summary>
    /// This class help to build a contract model to get proposal or purchase
    /// Through this methods that contains this class is posible
    /// create a contract by parameters
    /// </summary>
    public class ContractBuilder
    {
        private const string InvalidException = "This instance is not prepare to use this methods";

        /// <summary>
        /// This overload of the construct allow define initial parameters
        /// </summary>
        /// <param name="type"></param>
        /// <param name="option"></param>
        /// <param name="symbol"></param>
        public ContractBuilder(ContractOption type, bool option, string symbol = "")
        {
            _type = type;
            this.option = option;
            this.symbol = symbol;
        }

        /// <summary>
        /// parameters less construct
        /// </summary>
        public ContractBuilder()
        {
        }

        /// <summary>
        /// construct to just set the web socket service
        /// </summary>
        public ContractBuilder(WebSocketStream ws)
        {
            _ws = ws;
        }


        /// <summary>
        /// The internal reference to ws socket stream to use the some method
        /// </summary>
        internal WebSocketStream _ws;

        public ContractOption _type;
        private bool option;
        private string symbol;
        private readonly Parameters _parameter = new Parameters();

        /// <summary>
        /// Select the option to purchace in this contract
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public ContractBuilder Choice(bool option)
        {
            this.option = option;
            return this;
        }

        /// <summary>
        /// This method help to set the value parameters
        /// -- amount
        /// -- basis
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="basis"></param>
        /// <returns></returns>
        public ContractBuilder SetValue(double amount, Basis basis = Basis.Stake)
        {
            _parameter.Basis = basis;
            _parameter.Amount = amount;
            return this;
        }

        /// <summary>
        /// Set the value parameters to get a payout by amount argument
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public ContractBuilder ToPayout(double amount)
        {
            _parameter.Basis = Basis.Payout;
            _parameter.Amount = amount;
            return this;
        }

        /// <summary>
        /// Set the concurrency in the purchase
        /// </summary>
        /// <param name="concurrency"></param>
        /// <returns></returns>
        public ContractBuilder SetConcurrency(DConcurrency concurrency)
        {
            _parameter.Currency = concurrency.ToString();
            return this;
        }

        /// <summary>
        /// Set the concurrency in the purchase
        /// </summary>
        /// <param name="concurrency"></param>
        /// <returns></returns>
        public ContractBuilder SetConcurrency(string concurrency)
        {
            _parameter.Currency = concurrency;
            return this;
        }

        /// <summary>
        /// Set the duration in the purchase
        /// </summary>
        /// <param name="concurrency"></param>
        /// <returns></returns>
        public ContractBuilder SetDuration(DurationUnit unit, long duration)
        {
            _parameter.DurationUnit = unit;
            _parameter.Duration = duration;
            return this;
        }


        /// <summary>
        /// Set the duration in the purchase by date expiration
        /// </summary>
        /// <param name="concurrency"></param>
        /// <returns></returns>
        public ContractBuilder ExpireAt(DateTime date)
        {
            _parameter.DateExpiry = new DateTimeOffset(date).ToUnixTimeSeconds();
            return this;
        }

        /// <summary>
        /// Set the barries parameters to define a contract that use one or two barriers 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ContractBuilder SetBarriers(double first, double second = default)
        {
            _parameter.Barrier = GetBarrierNotation( first );
            
            if (!second.Equals(default))
            {
                _parameter.Barrier2 = GetBarrierNotation(second);
            }
            return this;
        }

        public ContractBuilder SetMarketSymbol(string symbol)
        {
            this.symbol = symbol;
            return this;
        }

        /// <summary>
        /// This use the <see cref="WebSocketStream"/> in the property <see cref="_ws"/> to
        /// make a request to purchase a contract
        /// </summary>
        /// <returns></returns>
        public async Task<ContractHandler> BuyAsync()
        {
            if (_ws is null)
            {
                throw new InvalidOperationException(InvalidException);
            }

            var response = await _ws.QueryAsync<BuyRequest, BuyResponse>(Build(), SerializerOptions.BuySerializer);

            // devolver el objeto de la compra
            return new ContractHandler(response.Buy, _ws);
        }

        /// <summary>
        /// This use the <see cref="WebSocketStream"/> in the property <see cref="_ws"/> to
        /// make a request to purchase a contract
        /// </summary>
        /// <returns></returns>
        public async Task<ContractHandler> BuyAsync(double amount, Basis basis = Basis.Stake)
        {
            if (_ws is null)
            {
                throw new InvalidOperationException(InvalidException);
            }

            var response = await _ws.QueryAsync<BuyRequest, BuyResponse>(Build(amount, basis), SerializerOptions.BuySerializer);

            // devolver el objeto de la compra
            return new ContractHandler(response.Buy, _ws);
        }

        /// <summary>
        /// Realiza la compra de un contrato
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public BuyRequest Build(double amount, Basis basis = Basis.Stake)
        {
            return new BuyRequest
            {
                Buy = "1",
                Parameters = new Parameters {
                    Duration = _parameter.Duration,
                    DurationUnit = _parameter.DurationUnit,
                    Barrier = (_parameter.Barrier != "0") ? _parameter.Barrier : null,
                    Barrier2 = this._parameter.Barrier2,
                    ContractType = MapContract(this._type, this.option),
                    Amount = amount,
                    Currency = this._parameter.Currency,
                    DateExpiry = _parameter.DateExpiry != default ? _parameter.DateExpiry : 0,
                    Basis = basis,
                    Symbol = symbol
                },
                Price = amount * 10
            };
        }

        /// <summary>
        /// Realiza la compra de un contrato
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public BuyRequest Build()
        {
            return new BuyRequest
            {
                Buy = "1",
                Parameters = new Parameters
                {
                    Duration = _parameter.Duration,
                    DurationUnit = _parameter.DurationUnit,
                    Barrier = (_parameter.Barrier != "0") ? _parameter.Barrier : null,
                    Barrier2 = this._parameter.Barrier2,
                    ContractType = MapContract(this._type, this.option),
                    Amount = _parameter.Amount,
                    Currency = this._parameter.Currency,
                    DateExpiry = _parameter.DateExpiry != default ? _parameter.DateExpiry : 0,
                    Basis = _parameter.Basis,
                    Symbol = symbol
                },
                Price = _parameter.Amount * 10
            };
        }

        /// <summary>
        /// Helper to define the contract map with <see cref="ContractType"/> object from the api
        /// </summary>
        /// <param name="type"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static ContractType MapContract(ContractOption type, bool option)
        {
            return type switch
            {
                ContractOption.HighLow when (option) => ContractType.Call,
                ContractOption.HighLow when(!option) => ContractType.Put,
                ContractOption.RiseDown when (option) => ContractType.Call,
                ContractOption.RiseDown when (!option) => ContractType.Put,
                ContractOption.Reset when (option) => ContractType.Resetcall,
                ContractOption.Reset when (!option) => ContractType.Resetput,
                ContractOption.TouchNotTouch when (option) => ContractType.Onetouch,
                ContractOption.TouchNotTouch when (!option) => ContractType.Notouch,
                ContractOption.DigitMatch when (option) => ContractType.Digitmatch,
                ContractOption.TouchNotTouch when (!option) => ContractType.Digitdiff,
                _ => default,
            };
        }

        /// <summary>
        /// Helper to build the barrier notation in string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetBarrierNotation(double value)
        {
            return string.Empty;
        }
    }
}
