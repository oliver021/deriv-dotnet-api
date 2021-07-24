namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Latest price and other details for a given contract
    /// </summary>
    public class ProposalResponse
    {
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
        /// Latest price and other details for a given contract
        /// </summary>
        [JsonProperty("proposal", NullValueHandling = NullValueHandling.Ignore)]
        public Proposal Proposal { get; set; }

        /// <summary>
        /// Optional field sent in request to map to response, present only when request contains
        /// `req_id`.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }

        /// <summary>
        /// For subscription requests only.
        /// </summary>
        [JsonProperty("subscription", NullValueHandling = NullValueHandling.Ignore)]
        public SubscriptionInformation Subscription { get; set; }
    }

    /// <summary>
    /// Latest price and other details for a given contract
    /// </summary>
    public class Proposal
    {
        /// <summary>
        /// The ask price.
        /// </summary>
        [JsonProperty("ask_price")]
        public double AskPrice { get; set; }

        /// <summary>
        /// Contains information about contract cancellation option.
        /// </summary>
        [JsonProperty("cancellation", NullValueHandling = NullValueHandling.Ignore)]
        public Cancellation Cancellation { get; set; }

        /// <summary>
        /// Commission changed in percentage (%).
        /// </summary>
        [JsonProperty("commission")]
        public double Commission { get; set; }

        /// <summary>
        /// The end date of the contract.
        /// </summary>
        [JsonProperty("date_expiry", NullValueHandling = NullValueHandling.Ignore)]
        public long? DateExpiry { get; set; }

        /// <summary>
        /// The start date of the contract.
        /// </summary>
        [JsonProperty("date_start")]
        public long DateStart { get; set; }

        /// <summary>
        /// Same as `ask_price`.
        /// </summary>
        [JsonProperty("display_value")]
        public string DisplayValue { get; set; }

        /// <summary>
        /// A per-connection unique identifier. Can be passed to the `forget` API call to unsubscribe.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Contains limit order information. (Only applicable for contract with limit order).
        /// </summary>
        [JsonProperty("limit_order", NullValueHandling = NullValueHandling.Ignore)]
        public LimitOrder LimitOrder { get; set; }

        /// <summary>
        /// Example: Win payout if Random 100 Index is strictly higher than entry spot at 15 minutes
        /// after contract start time.
        /// </summary>
        [JsonProperty("longcode")]
        public string Longcode { get; set; }

        /// <summary>
        /// [Only for lookback trades] Multiplier applies when calculating the final payoff for each
        /// type of lookback. e.g. (Exit spot - Lowest historical price) * multiplier = Payout
        /// </summary>
        [JsonProperty("multiplier", NullValueHandling = NullValueHandling.Ignore)]
        public double Multiplier { get; set; } = 0;

        /// <summary>
        /// The payout amount of the contract.
        /// </summary>
        [JsonProperty("payout")]
        public double Payout { get; set; }

        /// <summary>
        /// Spot value (if there are no Exchange data-feed licensing restrictions for the underlying
        /// symbol).
        /// </summary>
        [JsonProperty("spot")]
        public double Spot { get; set; }

        /// <summary>
        /// The corresponding time of the spot value.
        /// </summary>
        [JsonProperty("spot_time")]
        public long SpotTime { get; set; }
    }

    /// <summary>
    /// Contaner for proposal response serializer settings
    /// </summary>
    public static class ProposalResponseConverter
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
