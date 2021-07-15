namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Server status alongside general settings like call limits, currencies information,
    /// supported languages, etc.
    /// </summary>
    public partial class WebsiteStatusResponse
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
        /// For subscription requests only.
        /// </summary>
        [JsonProperty("subscription", NullValueHandling = NullValueHandling.Ignore)]
        public SubscriptionInformation Subscription { get; set; }

        /// <summary>
        /// Server status and other information regarding general settings
        /// </summary>
        [JsonProperty("website_status", NullValueHandling = NullValueHandling.Ignore)]
        public WebsiteStatus WebsiteStatus { get; set; }
    }

    /// <summary>
    /// Server status and other information regarding general settings
    /// </summary>
    public partial class WebsiteStatus
    {
        /// <summary>
        /// Maximum number of API calls during specified period of time.
        /// </summary>
        [JsonProperty("api_call_limits")]
        public ApiCallLimits ApiCallLimits { get; set; }

        /// <summary>
        /// Country code of connected IP
        /// </summary>
        [JsonProperty("clients_country", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientsCountry { get; set; }

        /// <summary>
        /// Provides minimum withdrawal for all crypto currency in USD
        /// </summary>
        [JsonProperty("crypto_config")]
        public Dictionary<string, object> CryptoConfig { get; set; }

        /// <summary>
        /// Available currencies and their information
        /// </summary>
        [JsonProperty("currencies_config")]
        public Dictionary<string, object> CurrenciesConfig { get; set; }

        /// <summary>
        /// Text for site status banner, contains problem description. shown only if set by the
        /// system.
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        /// Peer-to-peer payment system settings.
        /// </summary>
        [JsonProperty("p2p_config")]
        public P2PConfig P2PConfig { get; set; }

        /// <summary>
        /// The current status of the website.
        /// </summary>
        [JsonProperty("site_status", NullValueHandling = NullValueHandling.Ignore)]
        public SiteStatus? SiteStatus { get; set; }

        /// <summary>
        /// Provides codes for languages supported.
        /// </summary>
        [JsonProperty("supported_languages", NullValueHandling = NullValueHandling.Ignore)]
        public string[] SupportedLanguages { get; set; }

        /// <summary>
        /// Latest terms and conditions version.
        /// </summary>
        [JsonProperty("terms_conditions_version", NullValueHandling = NullValueHandling.Ignore)]
        public string TermsConditionsVersion { get; set; }
    }

    /// <summary>
    /// Maximum number of API calls during specified period of time.
    /// </summary>
    public partial class ApiCallLimits
    {
        /// <summary>
        /// Maximum subscription to proposal calls.
        /// </summary>
        [JsonProperty("max_proposal_subscription")]
        public MaxProposalSubscription MaxProposalSubscription { get; set; }

        /// <summary>
        /// Maximum number of general requests allowed during specified period of time.
        /// </summary>
        [JsonProperty("max_requestes_general")]
        public MaxRequestesGeneral MaxRequestesGeneral { get; set; }

        /// <summary>
        /// Maximum number of outcome requests allowed during specified period of time.
        /// </summary>
        [JsonProperty("max_requests_outcome")]
        public MaxRequestsOutcome MaxRequestsOutcome { get; set; }

        /// <summary>
        /// Maximum number of pricing requests allowed during specified period of time.
        /// </summary>
        [JsonProperty("max_requests_pricing")]
        public MaxRequestsPricing MaxRequestsPricing { get; set; }
    }

    /// <summary>
    /// Maximum subscription to proposal calls.
    /// </summary>
    public partial class MaxProposalSubscription
    {
        /// <summary>
        /// Describes which calls this limit applies to.
        /// </summary>
        [JsonProperty("applies_to")]
        public string AppliesTo { get; set; }

        /// <summary>
        /// Maximum number of allowed calls.
        /// </summary>
        [JsonProperty("max")]
        public double Max { get; set; }
    }

    /// <summary>
    /// Maximum number of general requests allowed during specified period of time.
    /// </summary>
    public partial class MaxRequestesGeneral
    {
        /// <summary>
        /// Describes which calls this limit applies to.
        /// </summary>
        [JsonProperty("applies_to")]
        public string AppliesTo { get; set; }

        /// <summary>
        /// The maximum of allowed calls per hour.
        /// </summary>
        [JsonProperty("hourly")]
        public double Hourly { get; set; }

        /// <summary>
        /// The maximum of allowed calls per minute.
        /// </summary>
        [JsonProperty("minutely")]
        public double Minutely { get; set; }
    }

    /// <summary>
    /// Maximum number of outcome requests allowed during specified period of time.
    /// </summary>
    public partial class MaxRequestsOutcome
    {
        /// <summary>
        /// Describes which calls this limit applies to.
        /// </summary>
        [JsonProperty("applies_to")]
        public string AppliesTo { get; set; }

        /// <summary>
        /// The maximum of allowed calls per hour.
        /// </summary>
        [JsonProperty("hourly")]
        public double Hourly { get; set; }

        /// <summary>
        /// The maximum of allowed calls per minute.
        /// </summary>
        [JsonProperty("minutely")]
        public double Minutely { get; set; }
    }

    /// <summary>
    /// Maximum number of pricing requests allowed during specified period of time.
    /// </summary>
    public partial class MaxRequestsPricing
    {
        /// <summary>
        /// Describes which calls this limit applies to.
        /// </summary>
        [JsonProperty("applies_to")]
        public string AppliesTo { get; set; }

        /// <summary>
        /// The maximum of allowed calls per hour.
        /// </summary>
        [JsonProperty("hourly")]
        public double Hourly { get; set; }

        /// <summary>
        /// The maximum of allowed calls per minute.
        /// </summary>
        [JsonProperty("minutely")]
        public double Minutely { get; set; }
    }

    /// <summary>
    /// Peer-to-peer payment system settings.
    /// </summary>
    public partial class P2PConfig
    {
        /// <summary>
        /// Maximum number of active ads allowed by an advertiser per currency pair and advert type
        /// (buy or sell).
        /// </summary>
        [JsonProperty("adverts_active_limit")]
        public double AdvertsActiveLimit { get; set; }

        /// <summary>
        /// Adverts will be deactivated if no activity occurs within this period, in days.
        /// </summary>
        [JsonProperty("adverts_archive_period", NullValueHandling = NullValueHandling.Ignore)]
        public double? AdvertsArchivePeriod { get; set; }

        /// <summary>
        /// A buyer will be blocked for this duration after exceeding the cancellation limit, in
        /// hours.
        /// </summary>
        [JsonProperty("cancellation_block_duration")]
        public double CancellationBlockDuration { get; set; }

        /// <summary>
        /// The period within which to count buyer cancellations, in hours.
        /// </summary>
        [JsonProperty("cancellation_count_period")]
        public double CancellationCountPeriod { get; set; }

        /// <summary>
        /// A buyer may cancel an order within this period without negative consequences, in minutes
        /// after order creation.
        /// </summary>
        [JsonProperty("cancellation_grace_period")]
        public double CancellationGracePeriod { get; set; }

        /// <summary>
        /// A buyer will be temporarily barred after marking this number of cancellations within
        /// cancellation_period.
        /// </summary>
        [JsonProperty("cancellation_limit")]
        public double CancellationLimit { get; set; }

        /// <summary>
        /// Maximum amount of an advert, in USD.
        /// </summary>
        [JsonProperty("maximum_advert_amount")]
        public double MaximumAdvertAmount { get; set; }

        /// <summary>
        /// Maximum amount of an order, in USD.
        /// </summary>
        [JsonProperty("maximum_order_amount")]
        public double MaximumOrderAmount { get; set; }

        /// <summary>
        /// Maximum number of orders a user may create per day.
        /// </summary>
        [JsonProperty("order_daily_limit")]
        public double OrderDailyLimit { get; set; }

        /// <summary>
        /// Time allowed for order payment, in minutes after order creation.
        /// </summary>
        [JsonProperty("order_payment_period")]
        public double OrderPaymentPeriod { get; set; }
    }

    /// <summary>
    /// The current status of the website.
    /// </summary>
    public enum SiteStatus { Down, Up, Updating };

    public static class B2Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                SiteStatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public class SiteStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SiteStatus) || t == typeof(SiteStatus?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "down":
                    return SiteStatus.Down;
                case "up":
                    return SiteStatus.Up;
                case "updating":
                    return SiteStatus.Updating;
            }
            throw new Exception("Cannot unmarshal type SiteStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SiteStatus)untypedValue;
            switch (value)
            {
                case SiteStatus.Down:
                    serializer.Serialize(writer, "down");
                    return;
                case SiteStatus.Up:
                    serializer.Serialize(writer, "up");
                    return;
                case SiteStatus.Updating:
                    serializer.Serialize(writer, "updating");
                    return;
            }
            throw new Exception("Cannot marshal type SiteStatus");
        }

        public static readonly SiteStatusConverter Singleton = new SiteStatusConverter();
    }
}
