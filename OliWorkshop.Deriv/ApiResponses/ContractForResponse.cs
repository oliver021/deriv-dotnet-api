namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using OliWorkshop.Deriv.ApiRequest;

    /// <summary>
    /// Get the list of currently available contracts
    /// </summary>
    public partial class ContractForResponse
    {
        /// <summary>
        /// List of available contracts. Note: if the user is authenticated, then only contracts
        /// allowed under his account will be returned.
        /// </summary>
        [JsonProperty("contracts_for", NullValueHandling = NullValueHandling.Ignore)]
        public ContractsList ContractsFor { get; set; }

        /// <summary>
        /// Echo of the request made.
        /// </summary>
        [JsonProperty("echo_req")]
        public Dictionary<string, object> EchoReq { get; set; }

        /// <summary>
        /// Action name of the request made.
        /// </summary>
        [JsonProperty("msg_type")]
        public string MsgType { get; set; }

        /// <summary>
        /// Optional field sent in request to map to response, present only when request contains
        /// `req_id`.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }
    }

    /// <summary>
    /// List of available contracts. Note: if the user is authenticated, then only contracts
    /// allowed under his account will be returned.
    /// </summary>
    public class ContractsList
    {
        /// <summary>
        /// Array of available contracts details
        /// </summary>
        [JsonProperty("available")]
        public ContractModel[] Available { get; set; }

        /// <summary>
        /// Symbol's next market-close time as an epoch value
        /// </summary>
        [JsonProperty("close")]
        public long? Close { get; set; }

        /// <summary>
        /// Indicates the feed license for symbol, for example whether its realtime or delayed
        /// </summary>
        [JsonProperty("feed_license", NullValueHandling = NullValueHandling.Ignore)]
        public string FeedLicense { get; set; }

        /// <summary>
        /// Count of contracts available
        /// </summary>
        [JsonProperty("hit_count")]
        public double HitCount { get; set; } = 0;

        /// <summary>
        /// Symbol's next market-open time as an epoch value
        /// </summary>
        [JsonProperty("open")]
        public long Open { get; set; }

        /// <summary>
        /// Current spot price for this underlying
        /// </summary>
        [JsonProperty("spot")]
        public double? Spot { get; set; }

        /// <summary>
        /// This prop is true if the multiplier is include
        /// </summary>
        public bool AllowMultipliers { get; }

        /// <summary>
        /// This prop is true if the only contract of multiplier is include
        /// </summary>
        public bool OnlyMultipleirs { get; }
    }

    /// <summary>
    /// Describe default parameters and condition to purchase a contract
    /// </summary>
    public partial class ContractModel
    {
        /// <summary>
        /// Array of available barriers for a predefined trading period
        /// </summary>
        [JsonProperty("available_barriers", NullValueHandling = NullValueHandling.Ignore)]
        public object[] AvailableBarriers { get; set; }

        /// <summary>
        /// Category of barrier.
        /// </summary>
        [JsonProperty("barrier_category")]
        public string BarrierCategory { get; set; }

        /// <summary>
        /// Number of barriers.
        /// </summary>
        [JsonProperty("barriers")]
        public double Barriers { get; set; }

        /// <summary>
        /// Category of contract.
        /// </summary>
        [JsonProperty("contract_category")]
        public string ContractCategory { get; set; }

        /// <summary>
        /// Category of the contract.
        /// </summary>
        [JsonProperty("contract_category_display")]
        public string ContractCategoryDisplay { get; set; }

        /// <summary>
        /// Display name for the type of contract.
        /// </summary>
        [JsonProperty("contract_display", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractDisplay { get; set; }

        /// <summary>
        /// Type of contract.
        /// </summary>
        [JsonProperty("contract_type")]
        public ContractType ContractType { get; set; }

        /// <summary>
        /// Name of exchange
        /// </summary>
        [JsonProperty("exchange_name")]
        public string ExchangeName { get; set; }

        /// <summary>
        /// Array of barriers already expired
        /// </summary>
        [JsonProperty("expired_barriers", NullValueHandling = NullValueHandling.Ignore)]
        public object[] ExpiredBarriers { get; set; }

        /// <summary>
        /// Expiry Type.
        /// </summary>
        [JsonProperty("expiry_type")]
        public string ExpiryType { get; set; }

        /// <summary>
        /// Array of returned forward starting options
        /// </summary>
        [JsonProperty("forward_starting_options", NullValueHandling = NullValueHandling.Ignore)]
        public ForwardStartingOption[] ForwardStartingOptions { get; set; }

        /// <summary>
        /// Type of market.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Maximum contract duration
        /// </summary>
        [JsonProperty("max_contract_duration")]
        public string MaxContractDuration { get; set; }

        /// <summary>
        /// Minimum contract duration.
        /// </summary>
        [JsonProperty("min_contract_duration")]
        public string MinContractDuration { get; set; }

        /// <summary>
        /// Maximum payout.
        /// </summary>
        [JsonProperty("payout_limit", NullValueHandling = NullValueHandling.Ignore)]
        public double? PayoutLimit { get; set; }

        /// <summary>
        /// Type of sentiment.
        /// </summary>
        [JsonProperty("sentiment")]
        public string Sentiment { get; set; }

        /// <summary>
        /// Start Type.
        /// </summary>
        [JsonProperty("start_type")]
        public string StartType { get; set; }

        /// <summary>
        /// Type of submarket.
        /// </summary>
        [JsonProperty("submarket")]
        public string Submarket { get; set; }

        /// <summary>
        /// A hash of predefined trading period
        /// </summary>
        [JsonProperty("trading_period", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> TradingPeriod { get; set; }

        /// <summary>
        /// Symbol code
        /// </summary>
        [JsonProperty("underlying_symbol")]
        public string UnderlyingSymbol { get; set; }
    }

    public partial class ForwardStartingOption
    {
        /// <summary>
        /// The epoch value for the closing date of forward starting session.
        /// </summary>
        [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
        public string Close { get; set; }

        /// <summary>
        /// The epoch value for the date of forward starting session.
        /// </summary>
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public string Date { get; set; }

        /// <summary>
        /// The epoch value for the opening date of forward starting session.
        /// </summary>
        [JsonProperty("open", NullValueHandling = NullValueHandling.Ignore)]
        public string Open { get; set; }
    }

    /// <summary>
    /// Action name of the request made.
    /// </summary>
    public static class ContractResponseForConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new ContractTypeConverter(),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
