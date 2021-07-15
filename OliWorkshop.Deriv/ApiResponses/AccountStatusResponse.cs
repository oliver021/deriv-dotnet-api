namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// A message with Account Status
    /// </summary>
    public partial class AccountStatusResponse
    {
        /// <summary>
        /// Echo of the request made.
        /// </summary>
        [JsonProperty("echo_req")]
        public Dictionary<string, object> EchoReq { get; set; }

        /// <summary>
        /// Account status details
        /// </summary>
        [JsonProperty("get_account_status", NullValueHandling = NullValueHandling.Ignore)]
        public GetAccountStatus GetAccountStatus { get; set; }

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
    }

    /// <summary>
    /// Account status details
    /// </summary>
    public partial class GetAccountStatus
    {
        /// <summary>
        /// This represents the authentication status of the user and it includes what authentication
        /// is needed.
        /// </summary>
        [JsonProperty("authentication", NullValueHandling = NullValueHandling.Ignore)]
        public Authentication Authentication { get; set; }

        /// <summary>
        /// If the cashier is unavailble, this array contains one or more error codes for each reason.
        /// </summary>
        [JsonProperty("cashier_validation", NullValueHandling = NullValueHandling.Ignore)]
        public string[] CashierValidation { get; set; }

        /// <summary>
        /// Provides cashier details for client currency.
        /// </summary>
        [JsonProperty("currency_config")]
        public Dictionary<string, object> CurrencyConfig { get; set; }

        /// <summary>
        /// Indicates whether the client should be prompted to authenticate their account.
        /// </summary>
        [JsonProperty("prompt_client_to_authenticate")]
        public long PromptClientToAuthenticate { get; set; }

        /// <summary>
        /// Client risk classification: `low`, `standard`, `high`.
        /// </summary>
        [JsonProperty("risk_classification")]
        public string RiskClassification { get; set; }

        /// <summary>
        /// Social identity provider a user signed up with.
        /// </summary>
        [JsonProperty("social_identity_provider", NullValueHandling = NullValueHandling.Ignore)]
        public SocialIdentityProvider? SocialIdentityProvider { get; set; }

        /// <summary>
        /// Account status. Possible status:
        /// - `address_verified`: client's address is verified by third party services.
        /// - `allow_document_upload`: client is allowed to upload documents.
        /// - `age_verification`: client is age-verified.
        /// - `authenticated`: client is fully authenticated.
        /// - `cashier_locked`: cashier is locked.
        /// - `closed`: client has closed the account.
        /// - `crs_tin_information`: client has updated tax related information.
        /// - `deposit_locked`: deposit is not allowed.
        /// - `disabled`: account is disabled.
        /// - `document_expired`: client's submitted proof-of-identity documents have expired.
        /// - `document_expiring_soon`: client's submitted proof-of-identity documents are expiring
        /// within a month.
        /// - `duplicate_account`: this client's account has been marked as duplicate.
        /// - `financial_assessment_not_complete`: client should complete their financial
        /// assessment.
        /// - `financial_information_not_complete`: client has not completed financial assessment.
        /// - `financial_risk_approval`: client has accepted financial risk disclosure.
        /// - `max_turnover_limit_not_set`: client has not set financial limits on their account.
        /// Applies to UK and Malta clients.
        /// - `mt5_withdrawal_locked`: MT5 deposits allowed, but withdrawal is not allowed.
        /// - `no_trading`: trading is disabled.
        /// - `no_withdrawal_or_trading`: client cannot trade or withdraw but can deposit.
        /// - `pa_withdrawal_explicitly_allowed`: withdrawal through payment agent is allowed.
        /// - `password_reset_required`: this client must reset their password.
        /// - `professional`: this client has opted for a professional account.
        /// - `professional_requested`: this client has requested for a professional account.
        /// - `professional_rejected`: this client's request for a professional account has been
        /// rejected.
        /// - `proveid_pending`: this client's identity is being validated. Applies for MX account
        /// with GB residence only.
        /// - `proveid_requested`: this client has made a request to have their identity be
        /// validated.
        /// - `social_signup`: this client is using social signup.
        /// - `trading_experience_not_complete`: client has not completed the trading experience
        /// questionnaire.
        /// - `ukgc_funds_protection`: client has acknowledged UKGC funds protection notice.
        /// - `unwelcome`: client cannot deposit or buy contracts, but can withdraw or sell
        /// contracts.
        /// - `withdrawal_locked`: deposits allowed but withdrawals are not allowed.
        /// </summary>
        [JsonProperty("status")]
        public string[] Status { get; set; }
    }

    /// <summary>
    /// This represents the authentication status of the user and it includes what authentication
    /// is needed.
    /// </summary>
    public partial class Authentication
    {
        /// <summary>
        /// The authentication status for document.
        /// </summary>
        [JsonProperty("document", NullValueHandling = NullValueHandling.Ignore)]
        public Document Document { get; set; }

        /// <summary>
        /// The authentication status for identity.
        /// </summary>
        [JsonProperty("identity", NullValueHandling = NullValueHandling.Ignore)]
        public Identity Identity { get; set; }

        /// <summary>
        /// An array containing the list of required authentication.
        /// </summary>
        [JsonProperty("needs_verification")]
        public string[] NeedsVerification { get; set; }
    }

    /// <summary>
    /// The authentication status for document.
    /// </summary>
    public partial class Document
    {
        /// <summary>
        /// This is the epoch of the document expiry date.
        /// </summary>
        [JsonProperty("expiry_date", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpiryDate { get; set; }

        /// <summary>
        /// This represents the current status of the proof of address document submitted for
        /// authentication.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public Status? Status { get; set; }
    }

    /// <summary>
    /// The authentication status for identity.
    /// </summary>
    public partial class Identity
    {
        /// <summary>
        /// This is the epoch of the document expiry date.
        /// </summary>
        [JsonProperty("expiry_date", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpiryDate { get; set; }

        /// <summary>
        /// This shows the information about the authentication services implemented
        /// </summary>
        [JsonProperty("services", NullValueHandling = NullValueHandling.Ignore)]
        public Services Services { get; set; }

        /// <summary>
        /// This represent the current status for proof of identity document submitted for
        /// authentication.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public Status? Status { get; set; }
    }

    /// <summary>
    /// This shows the information about the authentication services implemented
    /// </summary>
    public partial class Services
    {
        /// <summary>
        /// This shows the information related to Onfido supported services
        /// </summary>
        [JsonProperty("onfido", NullValueHandling = NullValueHandling.Ignore)]
        public Onfido Onfido { get; set; }
    }

    /// <summary>
    /// This shows the information related to Onfido supported services
    /// </summary>
    public partial class Onfido
    {
        /// <summary>
        /// 3 letter country code for Onfide SDK
        /// </summary>
        [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryCode { get; set; }

        /// <summary>
        /// This shows the list of documents types supported by Onfido
        /// </summary>
        [JsonProperty("documents", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Documents { get; set; }

        /// <summary>
        /// This shows the information if the country is supported by Onfido
        /// </summary>
        [JsonProperty("is_country_supported", NullValueHandling = NullValueHandling.Ignore)]
        public long? IsCountrySupported { get; set; }

        /// <summary>
        /// Show the last Onfido reported reasons for the rejected cases
        /// </summary>
        [JsonProperty("last_rejected", NullValueHandling = NullValueHandling.Ignore)]
        public string[] LastRejected { get; set; }

        /// <summary>
        /// Shows the latest document properties detected and reported by Onfido
        /// </summary>
        [JsonProperty("reported_properties", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> ReportedProperties { get; set; }

        /// <summary>
        /// This shows the number of Onfido submissions left for the client
        /// </summary>
        [JsonProperty("submissions_left", NullValueHandling = NullValueHandling.Ignore)]
        public long? SubmissionsLeft { get; set; }
    }

    /// <summary>
    /// This represents the current status of the proof of address document submitted for
    /// authentication.
    ///
    /// This represent the current status for proof of identity document submitted for
    /// authentication.
    /// </summary>
    public enum Status { Expired, None, Pending, Rejected, Suspected, Verified };

    /// <summary>
    /// Social identity provider a user signed up with.
    /// </summary>
    public enum SocialIdentityProvider { Apple, Facebook, Google };

 
    public static class AccountStatusConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                StatusConverter.Singleton,
                SocialIdentityProviderConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "expired":
                    return Status.Expired;
                case "none":
                    return Status.None;
                case "pending":
                    return Status.Pending;
                case "rejected":
                    return Status.Rejected;
                case "suspected":
                    return Status.Suspected;
                case "verified":
                    return Status.Verified;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.Expired:
                    serializer.Serialize(writer, "expired");
                    return;
                case Status.None:
                    serializer.Serialize(writer, "none");
                    return;
                case Status.Pending:
                    serializer.Serialize(writer, "pending");
                    return;
                case Status.Rejected:
                    serializer.Serialize(writer, "rejected");
                    return;
                case Status.Suspected:
                    serializer.Serialize(writer, "suspected");
                    return;
                case Status.Verified:
                    serializer.Serialize(writer, "verified");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }

    internal class SocialIdentityProviderConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SocialIdentityProvider) || t == typeof(SocialIdentityProvider?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "apple":
                    return SocialIdentityProvider.Apple;
                case "facebook":
                    return SocialIdentityProvider.Facebook;
                case "google":
                    return SocialIdentityProvider.Google;
            }
            throw new Exception("Cannot unmarshal type SocialIdentityProvider");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SocialIdentityProvider)untypedValue;
            switch (value)
            {
                case SocialIdentityProvider.Apple:
                    serializer.Serialize(writer, "apple");
                    return;
                case SocialIdentityProvider.Facebook:
                    serializer.Serialize(writer, "facebook");
                    return;
                case SocialIdentityProvider.Google:
                    serializer.Serialize(writer, "google");
                    return;
            }
            throw new Exception("Cannot marshal type SocialIdentityProvider");
        }

        public static readonly SocialIdentityProviderConverter Singleton = new SocialIdentityProviderConverter();
    }
}
