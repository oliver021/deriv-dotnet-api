namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Trading and Withdrawal Limits for a given user
    /// </summary>
    public partial class LimitRequest
    {
        /// <summary>
        /// Must be `1`
        /// </summary>
        [JsonProperty("get_limits")]
        public long GetLimits { get; set; }

        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }
    }
}
