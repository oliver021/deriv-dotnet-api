namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Return details of user account balance
    /// </summary>
    public partial class BalanceResponse
    {
        /// <summary>
        /// Current balance of one or more accounts.
        /// </summary>
        [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
        public Balance Balance { get; set; }

        /// <summary>
        /// Echo of the request made.
        /// </summary>
        [JsonProperty("echo_req")]
        public Dictionary<string, object> EchoReq { get; set; }

        /// <summary>
        /// Action name of the request made.
        /// </summary>
        [JsonProperty("msg_type")]
        public MsgTypeAccount MsgType { get; set; }

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
    }

    /// <summary>
    /// Current balance of one or more accounts.
    /// </summary>
    public partial class Balance
    {
        /// <summary>
        /// List of active accounts.
        /// </summary>
        [JsonProperty("accounts", NullValueHandling = NullValueHandling.Ignore)]
        public Accounts Accounts { get; set; }

        /// <summary>
        /// Balance of current account.
        /// </summary>
        [JsonProperty("balance")]
        [JsonConverter(typeof(MinMaxValueCheckConverter))]
        public double BalanceBalance { get; set; }

        /// <summary>
        /// Currency of current account.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// A per-connection unique identifier. Can be passed to the `forget` API call to unsubscribe.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// Client loginid.
        /// </summary>
        [JsonProperty("loginid")]
        public string Loginid { get; set; }

        /// <summary>
        /// Summary totals of accounts by type.
        /// </summary>
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public Total Total { get; set; }
    }

    /// <summary>
    /// List of active accounts.
    /// </summary>
    public partial class Accounts
    {
    }

    /// <summary>
    /// Summary totals of accounts by type.
    /// </summary>
    public partial class Total
    {
        /// <summary>
        /// Total balance of all real money Deriv accounts.
        /// </summary>
        [JsonProperty("deriv", NullValueHandling = NullValueHandling.Ignore)]
        public DerivBalance Deriv { get; set; }

        /// <summary>
        /// Total balance of all demo Deriv accounts.
        /// </summary>
        [JsonProperty("deriv_demo", NullValueHandling = NullValueHandling.Ignore)]
        public DerivDemo DerivDemo { get; set; }

        /// <summary>
        /// Total balance of all MT5 real money accounts.
        /// </summary>
        [JsonProperty("mt5", NullValueHandling = NullValueHandling.Ignore)]
        public Mt5 Mt5 { get; set; }

        /// <summary>
        /// Total balance of all MT5 demo accounts.
        /// </summary>
        [JsonProperty("mt5_demo", NullValueHandling = NullValueHandling.Ignore)]
        public Mt5Demo Mt5Demo { get; set; }
    }

    /// <summary>
    /// Total balance of all real money Deriv accounts.
    /// </summary>
    public partial class DerivBalance
    {
        /// <summary>
        /// Total of balances.
        /// </summary>
        [JsonProperty("amount")]
        [JsonConverter(typeof(MinMaxValueCheckConverter))]
        public double Amount { get; set; }

        /// <summary>
        /// Currency of total.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    /// <summary>
    /// Total balance of all demo Deriv accounts.
    /// </summary>
    public partial class DerivDemo
    {
        /// <summary>
        /// Total of balances.
        /// </summary>
        [JsonProperty("amount")]
        [JsonConverter(typeof(MinMaxValueCheckConverter))]
        public double Amount { get; set; }

        /// <summary>
        /// Currency of total.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    /// <summary>
    /// Total balance of all MT5 real money accounts.
    /// </summary>
    public partial class Mt5
    {
        /// <summary>
        /// Total balance of all MT5 accounts
        /// </summary>
        [JsonProperty("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Currency of total.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    /// <summary>
    /// Total balance of all MT5 demo accounts.
    /// </summary>
    public partial class Mt5Demo
    {
        /// <summary>
        /// Total of balances.
        /// </summary>
        [JsonProperty("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Currency of total.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    /// <summary>
    /// For subscription requests only.
    /// </summary>
    public partial class SubscriptionInformation
    {
        /// <summary>
        /// A per-connection unique identifier. Can be passed to the `forget` API call to unsubscribe.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    /// <summary>
    /// Action name of the request made.
    /// </summary>
    public enum MsgTypeAccount { Balance };

    internal static class ConverterAccount
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                MsgTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class MinMaxValueCheckConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(double) || t == typeof(double?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<double>(reader);
            if (value >= 0)
            {
                return value;
            }
            throw new Exception("Cannot unmarshal type double");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (double)untypedValue;
            if (value >= 0)
            {
                serializer.Serialize(writer, value);
                return;
            }
            throw new Exception("Cannot marshal type double");
        }

        public static readonly MinMaxValueCheckConverter Singleton = new MinMaxValueCheckConverter();
    }

    internal class MsgTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MsgTypeAccount) || t == typeof(MsgTypeAccount?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "balance")
            {
                return MsgTypeAccount.Balance;
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
            var value = (MsgTypeAccount)untypedValue;
            if (value == MsgTypeAccount.Balance)
            {
                serializer.Serialize(writer, "balance");
                return;
            }
            throw new Exception("Cannot marshal type MsgType");
        }

        public static readonly MsgTypeConverter Singleton = new MsgTypeConverter();
    }
}
