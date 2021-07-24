namespace OliWorkshop.Deriv.ApiResponse
{
    using Newtonsoft.Json;

    /// <summary>
    /// For subscription requests only.
    /// </summary>
    public class SubscriptionInformation
    {
        /// <summary>
        /// A per-connection unique identifier. Can be passed to the `forget` API call to unsubscribe.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
