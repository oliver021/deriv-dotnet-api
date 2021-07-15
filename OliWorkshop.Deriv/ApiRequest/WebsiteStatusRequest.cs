namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Request server status.
    /// </summary>
    public partial class WebsiteStatusRequest : TrackObject
    {
        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// [Optional] Used to map request to response.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }

        /// <summary>
        /// [Optional] `1` to stream the server/website status updates.
        /// </summary>
        [JsonProperty("subscribe", NullValueHandling = NullValueHandling.Ignore)]
        public long? Subscribe { get; set; }

        /// <summary>
        /// Must be `1`
        /// </summary>
        [JsonProperty("website_status")]
        public long WebsiteStatus { get; set; }
    }
}
