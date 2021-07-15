namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// A message with transaction results is received
    /// </summary>
    public partial class BuyResponse
    {
        /// <summary>
        /// Receipt confirmation for the purchase
        /// </summary>
        [JsonProperty("buy", NullValueHandling = NullValueHandling.Ignore)]
        public Buy Buy { get; set; }

        /// <summary>
        /// Receipt confirmation for the purchase
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorClass Error { get; set; }

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

        /// <summary>
        /// For subscription requests only.
        /// </summary>
        [JsonProperty("subscription", NullValueHandling = NullValueHandling.Ignore)]
        public SubscriptionInformation Subscription { get; set; }
    }

    /// <summary>
    /// Receipt confirmation for the purchase
    /// </summary>
    public partial class Buy
    {
        /// <summary>
        /// The new account balance after completion of the purchase
        /// </summary>
        [JsonProperty("balance_after")]
        public double BalanceAfter { get; set; }

        /// <summary>
        /// Actual effected purchase price
        /// </summary>
        [JsonProperty("buy_price")]
        public double BuyPrice { get; set; }

        /// <summary>
        /// Internal contract identifier
        /// </summary>
        [JsonProperty("contract_id")]
        public long ContractId { get; set; }

        /// <summary>
        /// The description of contract purchased
        /// </summary>
        [JsonProperty("longcode")]
        public string Longcode { get; set; }

        /// <summary>
        /// Proposed payout value
        /// </summary>
        [JsonProperty("payout")]
        public double Payout { get; set; }

        /// <summary>
        /// Epoch value of the transaction purchase time
        /// </summary>
        [JsonProperty("purchase_time")]
        public long PurchaseTime { get; set; }

        /// <summary>
        /// Compact description of the contract purchased
        /// </summary>
        [JsonProperty("shortcode")]
        public string Shortcode { get; set; }

        /// <summary>
        /// Epoch value showing the expected start time of the contract
        /// </summary>
        [JsonProperty("start_time")]
        public long StartTime { get; set; }

        /// <summary>
        /// Internal transaction identifier
        /// </summary>
        [JsonProperty("transaction_id")]
        public long TransactionId { get; set; }
    }

    /// <summary>
    /// Action name of the request made.
    /// </summary>
    public enum MsgTypeBuy { Buy };

    internal static class ConverterBuyResponse
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
