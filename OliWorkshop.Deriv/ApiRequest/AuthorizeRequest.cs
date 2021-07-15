namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Authorize current WebSocket session to act on behalf of the owner of a given token. Must
    /// precede requests that need to access client account, for example purchasing and selling
    /// contracts or viewing portfolio.
    /// </summary>
    public partial class AuthorizeRequest : TrackObject
    {
        /// <summary>
        /// [Optional] Send this when you use api tokens for authorization and want to track activity
        /// using `login_history` call.
        /// </summary>
        [JsonProperty("add_to_login_history", NullValueHandling = NullValueHandling.Ignore)]
        public long? AddToLoginHistory { get; set; }

        /// <summary>
        /// Authentication token. May be retrieved from
        /// https://www.binary.com/en/user/security/api_tokenws.html
        /// </summary>
        [JsonProperty("authorize")]
        public string Authorize { get; set; }

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
    }
}

