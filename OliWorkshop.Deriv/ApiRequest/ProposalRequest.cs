namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using OliWorkshop.Deriv.ApiRequests;

    /// <summary>
    /// Gets latest price for a specific contract.
    /// </summary>
    public partial class ProposalRequest
    {
        /// <summary>
        /// [Optional] Proposed contract payout or stake, or multiplier (for lookbacks).
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(MinMaxValueCheckConverter))]
        public double? Amount { get; set; }

        /// <summary>
        /// [Optional] Barrier for the contract (or last digit prediction for digit contracts).
        /// Contracts less than 24 hours in duration would need a relative barrier (barriers which
        /// need +/-), where entry spot would be adjusted accordingly with that amount to define a
        /// barrier, except for Synthetic Indices as they support both relative and absolute
        /// barriers. Not needed for lookbacks.
        /// </summary>
        [JsonProperty("barrier", NullValueHandling = NullValueHandling.Ignore)]
        public string Barrier { get; set; }

        /// <summary>
        /// [Optional] Low barrier for the contract (for contracts with two barriers). Contracts less
        /// than 24 hours in duration would need a relative barrier (barriers which need +/-), where
        /// entry spot would be adjusted accordingly with that amount to define a barrier, except for
        /// Synthetic Indices as they support both relative and absolute barriers. Not needed for
        /// lookbacks.
        /// </summary>
        [JsonProperty("barrier2", NullValueHandling = NullValueHandling.Ignore)]
        public string Barrier2 { get; set; }

        /// <summary>
        /// [Optional] Indicates type of the `amount`.
        /// </summary>
        [JsonProperty("basis", NullValueHandling = NullValueHandling.Ignore)]
        public Basis? Basis { get; set; }

        /// <summary>
        /// Cancellation duration option (only for `MULTUP` and `MULTDOWN` contracts).
        /// </summary>
        [JsonProperty("cancellation", NullValueHandling = NullValueHandling.Ignore)]
        public string Cancellation { get; set; }

        /// <summary>
        /// The proposed contract type
        /// </summary>
        [JsonProperty("contract_type")]
        public ContractType ContractType { get; set; }

        /// <summary>
        /// This can only be the account-holder's currency (obtained from `payout_currencies` call).
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// [Optional] Epoch value of the expiry time of the contract. Either date_expiry or duration
        /// is required.
        /// </summary>
        [JsonProperty("date_expiry", NullValueHandling = NullValueHandling.Ignore)]
        public long DateExpiry { get; set; } = 0;

        /// <summary>
        /// [Optional] Indicates epoch value of the starting time of the contract. If left empty, the
        /// start time of the contract is now.
        /// </summary>
        [JsonProperty("date_start", NullValueHandling = NullValueHandling.Ignore)]
        public long DateStart { get; set; } = 0;

        /// <summary>
        /// [Optional] Duration quantity. Either date_expiry or duration is required.
        /// </summary>
        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public long Duration { get; set; } = 0;

        /// <summary>
        /// [Optional] Duration unit - `s`: seconds, `m`: minutes, `h`: hours, `d`: days, `t`: ticks.
        /// </summary>
        [JsonProperty("duration_unit", NullValueHandling = NullValueHandling.Ignore)]
        public DurationUnit DurationUnit { get; set; }

        /// <summary>
        /// Add an order to close the contract once the order condition is met (only for `MULTUP` and
        /// `MULTDOWN` contracts). Supported orders: `take_profit`, `stop_loss`.
        /// </summary>
        [JsonProperty("limit_order", NullValueHandling = NullValueHandling.Ignore)]
        public LimitOrder LimitOrder { get; set; }

        /// <summary>
        /// [Optional] The multiplier for non-binary options. E.g. lookbacks.
        /// </summary>
        [JsonProperty("multiplier", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(MinMaxValueCheckConverter))]
        public double Multiplier { get; set; } = 0;

        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// [Optional] The product type.
        /// </summary>
        [JsonProperty("product_type", NullValueHandling = NullValueHandling.Ignore)]
        public ProductType ProductType { get; set; } = ProductType.Basic;

        /// <summary>
        /// Must be `1`
        /// </summary>
        [JsonProperty("proposal")]
        public long Proposal { get; set; }

        /// <summary>
        /// [Optional] Used to map request to response.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }

        /// <summary>
        /// [Optional] The tick that is predicted to have the highest/lowest value - for `TICKHIGH`
        /// and `TICKLOW` contracts.
        /// </summary>
        [JsonProperty("selected_tick", NullValueHandling = NullValueHandling.Ignore)]
        public long? SelectedTick { get; set; }

        /// <summary>
        /// [Optional] 1 - to initiate a realtime stream of prices. Note that tick trades (without a
        /// user-defined barrier), digit trades and less than 24 hours at-the-money contracts for the
        /// following underlying symbols are not streamed: `R_10`, `R_25`, `R_50`, `R_75`, `R_100`,
        /// `RDBULL`, `RDBEAR` (this is because their price is constant).
        /// </summary>
        [JsonProperty("subscribe", NullValueHandling = NullValueHandling.Ignore)]
        public long? Subscribe { get; set; }

        /// <summary>
        /// The short symbol name (obtained from `active_symbols` call).
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// [Optional] Required only for multi-barrier trading. Defines the epoch value of the
        /// trading period start time.
        /// </summary>
        [JsonProperty("trading_period_start", NullValueHandling = NullValueHandling.Ignore)]
        public long? TradingPeriodStart { get; set; }
    }


    /// <summary>
    /// Proposal container of the basic setting to serialzie request
    /// </summary>
    public static class ProposalRequestConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                BasisConverter.Singleton,
                ContractTypeConverter.Singleton,
                DurationUnitConverter.Singleton,
                ProductTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
