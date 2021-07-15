namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Get the list of servers for platform. Currently, only mt5 is supported
    /// </summary>
    public partial class TradingServerRequest
    {
        /// <summary>
        /// [Optional] Trading account type.
        /// </summary>
        [JsonProperty("account_type", NullValueHandling = NullValueHandling.Ignore)]
        public AccountType? AccountType { get; set; }

        /// <summary>
        /// [Optional] Pass the environment (installation) instance. Currently, there are one demo
        /// and two real environments. Defaults to 'all'.
        /// </summary>
        [JsonProperty("environment", NullValueHandling = NullValueHandling.Ignore)]
        public Environment? Environment { get; set; }

        /// <summary>
        /// [Optional] Market type.
        /// </summary>
        [JsonProperty("market_type", NullValueHandling = NullValueHandling.Ignore)]
        public MarketType? MarketType { get; set; }

        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// [Optional] Pass the trading platform name, default to mt5
        /// </summary>
        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public Platform? Platform { get; set; }

        /// <summary>
        /// [Optional] Used to map request to response.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }

        /// <summary>
        /// Must be `1`
        /// </summary>
        [JsonProperty("trading_servers")]
        public long TradingServers { get; set; }
    }

    /// <summary>
    /// [Optional] Trading account type.
    /// </summary>
    public enum AccountType { Demo, Real };

    /// <summary>
    /// [Optional] Pass the environment (installation) instance. Currently, there are one demo
    /// and two real environments. Defaults to 'all'.
    /// </summary>
    public enum Environment { All, DerivDemo, DerivServer, DerivServer02 };

    /// <summary>
    /// [Optional] Market type.
    /// </summary>
    public enum MarketType { Financial, Synthetic };

    /// <summary>
    /// [Optional] Pass the trading platform name, default to mt5
    /// </summary>
    public enum Platform { Mt5 };

    internal static class TradingServerConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                AccountTypeConverter.Singleton,
                EnvironmentConverter.Singleton,
                MarketTypeConverter.Singleton,
                PlatformConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class AccountTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AccountType) || t == typeof(AccountType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "demo":
                    return AccountType.Demo;
                case "real":
                    return AccountType.Real;
            }
            throw new Exception("Cannot unmarshal type AccountType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AccountType)untypedValue;
            switch (value)
            {
                case AccountType.Demo:
                    serializer.Serialize(writer, "demo");
                    return;
                case AccountType.Real:
                    serializer.Serialize(writer, "real");
                    return;
            }
            throw new Exception("Cannot marshal type AccountType");
        }

        public static readonly AccountTypeConverter Singleton = new AccountTypeConverter();
    }

    internal class EnvironmentConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Environment) || t == typeof(Environment?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Deriv-Demo":
                    return Environment.DerivDemo;
                case "Deriv-Server":
                    return Environment.DerivServer;
                case "Deriv-Server-02":
                    return Environment.DerivServer02;
                case "all":
                    return Environment.All;
            }
            throw new Exception("Cannot unmarshal type Environment");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Environment)untypedValue;
            switch (value)
            {
                case Environment.DerivDemo:
                    serializer.Serialize(writer, "Deriv-Demo");
                    return;
                case Environment.DerivServer:
                    serializer.Serialize(writer, "Deriv-Server");
                    return;
                case Environment.DerivServer02:
                    serializer.Serialize(writer, "Deriv-Server-02");
                    return;
                case Environment.All:
                    serializer.Serialize(writer, "all");
                    return;
            }
            throw new Exception("Cannot marshal type Environment");
        }

        public static readonly EnvironmentConverter Singleton = new EnvironmentConverter();
    }

    /// <summary>
    /// The options for trading server
    /// </summary>
    public class MarketTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MarketType) || t == typeof(MarketType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "financial":
                    return MarketType.Financial;
                case "synthetic":
                    return MarketType.Synthetic;
            }
            throw new Exception("Cannot unmarshal type MarketType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (MarketType)untypedValue;
            switch (value)
            {
                case MarketType.Financial:
                    serializer.Serialize(writer, "financial");
                    return;
                case MarketType.Synthetic:
                    serializer.Serialize(writer, "synthetic");
                    return;
            }
            throw new Exception("Cannot marshal type MarketType");
        }

        public static readonly MarketTypeConverter Singleton = new MarketTypeConverter();
    }

    internal class PlatformConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Platform) || t == typeof(Platform?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "mt5")
            {
                return Platform.Mt5;
            }
            throw new Exception("Cannot unmarshal type Platform");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Platform)untypedValue;
            if (value == Platform.Mt5)
            {
                serializer.Serialize(writer, "mt5");
                return;
            }
            throw new Exception("Cannot marshal type Platform");
        }

        public static readonly PlatformConverter Singleton = new PlatformConverter();
    }
}
