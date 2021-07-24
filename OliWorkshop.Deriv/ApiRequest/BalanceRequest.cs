namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using OliWorkshop.Deriv;

    /// <summary>
    /// Get user account balance
    /// </summary>
    public class BalanceRequest : TrackObject
    {
        /// <summary>
        /// [Optional] If set to `all`, return the balances of all accounts one by one; if set to
        /// `current`, return the balance of current account; if set as an account id, return the
        /// balance of that account.
        /// </summary>
        [JsonProperty("account", NullValueHandling = NullValueHandling.Ignore)]
        public string Account { get; set; }

        /// <summary>
        /// Must be `1`
        /// </summary>
        [JsonProperty("balance")]
        public long Balance { get; set; } = 1;

        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// [Optional] If set to 1, will send updates whenever the balance changes.
        /// </summary>
        [JsonProperty("subscribe", NullValueHandling = NullValueHandling.Ignore)]
        public long? Subscribe { get; set; }
    }
}
