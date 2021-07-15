namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Trading and Withdrawal Limits
    /// </summary>
    public partial class LimitResponse
    {
        /// <summary>
        /// Echo of the request made.
        /// </summary>
        [JsonProperty("echo_req")]
        public Dictionary<string, object> EchoReq { get; set; }

        /// <summary>
        /// Trading limits of real account user
        /// </summary>
        [JsonProperty("get_limits", NullValueHandling = NullValueHandling.Ignore)]
        public GetLimits GetLimits { get; set; }

        /// <summary>
        /// Action name of the request made.
        /// </summary>
        [JsonProperty("msg_type")]
        public MsgType MsgType { get; set; }

        /// <summary>
        /// Optional field sent in request to map to response, present only when request contains
        /// `req_id`.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }
    }

    /// <summary>
    /// Trading limits of real account user
    /// </summary>
    public partial class GetLimits
    {
        /// <summary>
        /// Maximum account cash balance
        /// </summary>
        [JsonProperty("account_balance", NullValueHandling = NullValueHandling.Ignore)]
        public double? AccountBalance { get; set; }

        /// <summary>
        /// Maximum daily turnover
        /// </summary>
        [JsonProperty("daily_turnover", NullValueHandling = NullValueHandling.Ignore)]
        public double? DailyTurnover { get; set; }

        /// <summary>
        /// Lifetime withdrawal limit
        /// </summary>
        [JsonProperty("lifetime_limit", NullValueHandling = NullValueHandling.Ignore)]
        public double? LifetimeLimit { get; set; }

        /// <summary>
        /// Contains limitation information for each market.
        /// </summary>
        [JsonProperty("market_specific", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> MarketSpecific { get; set; }

        /// <summary>
        /// Number of days for num_of_days_limit withdrawal limit
        /// </summary>
        [JsonProperty("num_of_days", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumOfDays { get; set; }

        /// <summary>
        /// Withdrawal limit for num_of_days days
        /// </summary>
        [JsonProperty("num_of_days_limit", NullValueHandling = NullValueHandling.Ignore)]
        public double? NumOfDaysLimit { get; set; }

        /// <summary>
        /// Maximum number of open positions
        /// </summary>
        [JsonProperty("open_positions", NullValueHandling = NullValueHandling.Ignore)]
        public long? OpenPositions { get; set; }

        /// <summary>
        /// Maximum aggregate payouts on open positions
        /// </summary>
        [JsonProperty("payout", NullValueHandling = NullValueHandling.Ignore)]
        public double? Payout { get; set; }

        /// <summary>
        /// Maximum payout for each symbol based on different barrier types.
        /// </summary>
        [JsonProperty("payout_per_symbol")]
        public PayoutPerSymbol PayoutPerSymbol { get; set; }

        /// <summary>
        /// Maximum aggregate payouts on open positions per symbol and contract type. This limit can
        /// be exceeded up to the overall payout limit if there is no prior open position.
        /// </summary>
        [JsonProperty("payout_per_symbol_and_contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public double? PayoutPerSymbolAndContractType { get; set; }

        /// <summary>
        /// Amount left to reach withdrawal limit
        /// </summary>
        [JsonProperty("remainder", NullValueHandling = NullValueHandling.Ignore)]
        public double? Remainder { get; set; }

        /// <summary>
        /// Total withdrawal for num_of_days days
        /// </summary>
        [JsonProperty("withdrawal_for_x_days_monetary", NullValueHandling = NullValueHandling.Ignore)]
        public double? WithdrawalForXDaysMonetary { get; set; }

        /// <summary>
        /// Total withdrawal since inception
        /// </summary>
        [JsonProperty("withdrawal_since_inception_monetary", NullValueHandling = NullValueHandling.Ignore)]
        public double? WithdrawalSinceInceptionMonetary { get; set; }
    }

    public partial class PayoutPerSymbol
    {
        /// <summary>
        /// Maximum aggregate payouts on open positions per symbol for contracts where barrier is
        /// same as entry spot.
        /// </summary>
        [JsonProperty("atm")]
        public double? Atm { get; set; }

        /// <summary>
        /// Maximum aggregate payouts on open positions per symbol for contract where barrier is
        /// different from entry spot.
        /// </summary>
        [JsonProperty("non_atm", NullValueHandling = NullValueHandling.Ignore)]
        public NonAtm NonAtm { get; set; }
    }

    /// <summary>
    /// Maximum aggregate payouts on open positions per symbol for contract where barrier is
    /// different from entry spot.
    /// </summary>
    public partial class NonAtm
    {
        /// <summary>
        /// Maximum aggregate payouts on open positions per symbol for contract where barrier is
        /// different from entry spot and duration is less than and equal to seven days
        /// </summary>
        [JsonProperty("less_than_seven_days", NullValueHandling = NullValueHandling.Ignore)]
        public double? LessThanSevenDays { get; set; }

        /// <summary>
        /// Maximum aggregate payouts on open positions per symbol for contract where barrier is
        /// different from entry spot and duration is more to seven days
        /// </summary>
        [JsonProperty("more_than_seven_days", NullValueHandling = NullValueHandling.Ignore)]
        public double? MoreThanSevenDays { get; set; }
    }


    public static class LimitResponseConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
