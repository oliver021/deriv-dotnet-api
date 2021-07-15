namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Status of set account currency call
    /// </summary>
    public partial class SetConcurrencyResponse
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
        /// `1`: success, `0`: no change
        /// </summary>
        [JsonProperty("set_account_currency", NullValueHandling = NullValueHandling.Ignore)]
        public long? SetAccountCurrency { get; set; }
    }

}
