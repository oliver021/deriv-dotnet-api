namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Get latest price (and other information) for a contract in the user's portfolio
    /// </summary>
    public partial class ProposalOpenRequest : TrackObject
    {
        /// <summary>
        /// [Optional] Contract ID received from a `portfolio` request. If not set, you will receive
        /// stream of all open contracts.
        /// </summary>
        [JsonProperty("contract_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ContractId { get; set; }

        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// Must be `1`
        /// </summary>
        [JsonProperty("proposal_open_contract")]
        public long ProposalOpenContract { get; set; }

        /// <summary>
        /// [Optional] Used to map request to response.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }

        /// <summary>
        /// [Optional] `1` to stream.
        /// </summary>
        [JsonProperty("subscribe", NullValueHandling = NullValueHandling.Ignore)]
        public long? Subscribe { get; set; }
    }
}
