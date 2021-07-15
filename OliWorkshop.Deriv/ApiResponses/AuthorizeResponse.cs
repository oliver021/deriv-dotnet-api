namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// A message containing account information for the holder of that token.
    /// </summary>
    public partial class AuthorizeResponse
    {
        /// <summary>
        /// Account information for the holder of the token.
        /// </summary>
        [JsonProperty("authorize", NullValueHandling = NullValueHandling.Ignore)]
        public Authorize Authorize { get; set; }

        /// <summary>
        /// Echo of the request made.
        /// </summary>
        [JsonProperty("echo_req")]
        public Dictionary<string, object> EchoReq { get; set; }

        /// <summary>
        /// Action name of the request made.
        /// </summary>
        [JsonProperty("msg_type")]
        public AuthorizeMsgType MsgType { get; set; }

        /// <summary>
        /// Optional field sent in request to map to response, present only when request contains
        /// `req_id`.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }
    }

    /// <summary>
    /// Account information for the holder of the token.
    /// </summary>
    public partial class Authorize
    {
        /// <summary>
        /// List of accounts for current user.
        /// </summary>
        [JsonProperty("account_list", NullValueHandling = NullValueHandling.Ignore)]
        public AccountList[] AccountList { get; set; }

        /// <summary>
        /// Cash balance of the account.
        /// </summary>
        [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
        public double? Balance { get; set; }

        /// <summary>
        /// 2-letter country code (ISO standard).
        /// </summary>
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        /// <summary>
        /// Currency of the account.
        /// </summary>
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        /// <summary>
        /// User email.
        /// </summary>
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        /// <summary>
        /// User's full name. Will be empty for virtual accounts.
        /// </summary>
        [JsonProperty("fullname", NullValueHandling = NullValueHandling.Ignore)]
        public string Fullname { get; set; }

        /// <summary>
        /// Boolean value: 1 or 0, indicating whether the account is a virtual-money account.
        /// </summary>
        [JsonProperty("is_virtual", NullValueHandling = NullValueHandling.Ignore)]
        public long? IsVirtual { get; set; }

        /// <summary>
        /// Landing company name the account belongs to.
        /// </summary>
        [JsonProperty("landing_company_fullname", NullValueHandling = NullValueHandling.Ignore)]
        public string LandingCompanyFullname { get; set; }

        /// <summary>
        /// Landing company shortcode the account belongs to.
        /// </summary>
        [JsonProperty("landing_company_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LandingCompanyName { get; set; }

        /// <summary>
        /// Currencies in client's residence country
        /// </summary>
        [JsonProperty("local_currencies", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> LocalCurrencies { get; set; }

        /// <summary>
        /// The account ID that the token was issued for.
        /// </summary>
        [JsonProperty("loginid", NullValueHandling = NullValueHandling.Ignore)]
        public string Loginid { get; set; }

        /// <summary>
        /// User's preferred language, ISO standard code of language
        /// </summary>
        [JsonProperty("preferred_language")]
        public string PreferredLanguage { get; set; }

        /// <summary>
        /// Scopes available to the token.
        /// </summary>
        [JsonProperty("scopes", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Scopes { get; set; }

        /// <summary>
        /// List of landing company shortcodes the account can upgrade to.
        /// </summary>
        [JsonProperty("upgradeable_landing_companies", NullValueHandling = NullValueHandling.Ignore)]
        public object[] UpgradeableLandingCompanies { get; set; }

        /// <summary>
        /// The internal user ID for this account.
        /// </summary>
        [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? UserId { get; set; }
    }

    public partial class AccountList
    {
        /// <summary>
        /// Currency of specified account.
        /// </summary>
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        /// <summary>
        /// Epoch of date till client has excluded him/herself from the website, only present if
        /// client is self excluded.
        /// </summary>
        [JsonProperty("excluded_until", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExcludedUntil { get; set; }

        /// <summary>
        /// Boolean value: 1 or 0, indicating whether the account is marked as disabled or not.
        /// </summary>
        [JsonProperty("is_disabled", NullValueHandling = NullValueHandling.Ignore)]
        public long? IsDisabled { get; set; }

        /// <summary>
        /// Boolean value: 1 or 0, indicating whether the account is a virtual-money account.
        /// </summary>
        [JsonProperty("is_virtual", NullValueHandling = NullValueHandling.Ignore)]
        public long? IsVirtual { get; set; }

        /// <summary>
        /// Landing company shortcode the account belongs to.
        /// </summary>
        [JsonProperty("landing_company_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LandingCompanyName { get; set; }

        /// <summary>
        /// The account ID of specified account.
        /// </summary>
        [JsonProperty("loginid", NullValueHandling = NullValueHandling.Ignore)]
        public string Loginid { get; set; }
    }

    /// <summary>
    /// Action name of the request made.
    /// </summary>
    public enum AuthorizeMsgType { Authorize };

    internal class AuthorizeMsgTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AuthorizeMsgType) || t == typeof(AuthorizeMsgType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "authorize")
            {
                return AuthorizeMsgType.Authorize;
            }
            throw new Exception("Cannot unmarshal type MsgType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AuthorizeMsgType)untypedValue;
            if (value == AuthorizeMsgType.Authorize)
            {
                serializer.Serialize(writer, "authorize");
                return;
            }
            throw new Exception("Cannot marshal type MsgType");
        }

        public static readonly MsgTypeConverter Singleton = new MsgTypeConverter();
    }
}
