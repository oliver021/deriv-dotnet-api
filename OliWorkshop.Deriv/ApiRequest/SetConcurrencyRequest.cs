namespace OliWorkshop.Deriv.ApiRequest
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Set account currency, this will be default currency for your account i.e currency for
    /// trading, deposit. Please note that account currency can only be set once, and then can
    /// never be changed.
    /// </summary>
    public partial class SetConcurrencyRequest
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
        /// Currency of the account. List of supported currencies can be acquired with
        /// `payout_currencies` call.
        /// </summary>
        [JsonProperty("set_account_currency")]
        public string SetAccountCurrency { get; set; }
    }
}
