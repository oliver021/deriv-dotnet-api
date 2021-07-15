namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The result of server time request.
    /// </summary>
    public partial class TimeResponse
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
        public MsgType MsgType { get; set; }

        /// <summary>
        /// Optional field sent in request to map to response, present only when request contains
        /// `req_id`.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }

        /// <summary>
        /// Epoch of server time.
        /// </summary>
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public long? Time { get; set; }
    }
}
