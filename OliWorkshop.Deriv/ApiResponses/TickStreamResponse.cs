namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Latest spot price for a given symbol. Continuous responses with a frequency of up to one
    /// second.
    /// </summary>
    public class TicksResponse
    {
        /// <summary>
        /// Echo of the request made.
        /// </summary>
        [JsonProperty("echo_req")]
        public Dictionary<string, object> EchoReq { get; set; }

        /// <summary>
        /// Type of the response.
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

        /// <summary>
        /// Tick by tick list of streamed data
        /// </summary>
        [JsonProperty("tick", NullValueHandling = NullValueHandling.Ignore)]
        public TickSpotData Tick { get; set; }
    }

    /// <summary>
    /// Tick by tick list of streamed data
    /// </summary>
    public partial class TickSpotData
    {
        /// <summary>
        /// Market ask at the epoch
        /// </summary>
        [JsonProperty("ask", NullValueHandling = NullValueHandling.Ignore)]
        public double? Ask { get; set; }

        /// <summary>
        /// Market bid at the epoch
        /// </summary>
        [JsonProperty("bid", NullValueHandling = NullValueHandling.Ignore)]
        public double? Bid { get; set; }

        /// <summary>
        /// Epoch time of the tick
        /// </summary>
        [JsonProperty("epoch", NullValueHandling = NullValueHandling.Ignore)]
        public long? Epoch { get; set; }

        /// <summary>
        /// A per-connection unique identifier. Can be passed to the `forget` API call to unsubscribe.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// Indicates the number of decimal points that the returned amounts must be displayed with
        /// </summary>
        [JsonProperty("pip_size")]
        public double PipSize { get; set; }

        /// <summary>
        /// Market value at the epoch
        /// </summary>
        [JsonProperty("quote", NullValueHandling = NullValueHandling.Ignore)]
        public double? Quote { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

    }

    public static class ConverterTicksResponse
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
