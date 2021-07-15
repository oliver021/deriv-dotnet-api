namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Sell a Contract as identified from a previous `portfolio` call.
    /// </summary>
    public partial class SellRequest : TrackObject
    {
        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// Minimum price at which to sell the contract, or `0` for 'sell at market'.
        /// </summary>
        [JsonProperty("price")]
        public double Price { get; set; }

        /// <summary>
        /// [Optional] Used to map request to response.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }

        /// <summary>
        /// Pass contract_id received from the `portfolio` call.
        /// </summary>
        [JsonProperty("sell")]
        public long Sell { get; set; }
    }
}

