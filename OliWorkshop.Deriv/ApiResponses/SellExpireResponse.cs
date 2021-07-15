namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The result of sell expired contract
    /// </summary>
    public partial class SellExpireResponse
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
        /// Optional field sent in request to map to response, present only when request contains
        /// `req_id`.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }

        /// <summary>
        /// Sell expired contract object containing count of contracts sold
        /// </summary>
        [JsonProperty("sell_expired", NullValueHandling = NullValueHandling.Ignore)]
        public SellExpired SellExpired { get; set; }
    }

    /// <summary>
    /// Sell expired contract object containing count of contracts sold
    /// </summary>
    public partial class SellExpired
    {
        /// <summary>
        /// The number of contracts that has been sold.
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }
    }


    public static class SellExpireConverter
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
