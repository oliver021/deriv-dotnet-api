namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Get list of servers for the platform provided.
    /// </summary>
    public partial class TradingServerResponse
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
        public string MsgType { get; set; }

        /// <summary>
        /// Optional field sent in request to map to response, present only when request contains
        /// `req_id`.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }

        /// <summary>
        /// Array containing platform server objects.
        /// </summary>
        [JsonProperty("trading_servers", NullValueHandling = NullValueHandling.Ignore)]
        public DetailsOfEachServer[] TradingServers { get; set; }
    }

    public partial class DetailsOfEachServer
    {
        /// <summary>
        /// Flag to represent if this server is currently disabled or not
        /// </summary>
        [JsonProperty("disabled", NullValueHandling = NullValueHandling.Ignore)]
        public long? Disabled { get; set; }

        /// <summary>
        /// Current environment (installation instance) where servers are deployed. Currently, there
        /// are one demo and two real environments.
        /// </summary>
        [JsonProperty("environment", NullValueHandling = NullValueHandling.Ignore)]
        public Environment? Environment { get; set; }

        /// <summary>
        /// Object containing geolocation information of the server.
        /// </summary>
        [JsonProperty("geolocation", NullValueHandling = NullValueHandling.Ignore)]
        public Geolocation Geolocation { get; set; }

        /// <summary>
        /// Server unique id.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public Id? Id { get; set; }

        /// <summary>
        /// Error message to client when server is disabled
        /// </summary>
        [JsonProperty("message_to_client", NullValueHandling = NullValueHandling.Ignore)]
        public string MessageToClient { get; set; }

        /// <summary>
        /// Flag to represent if this is server is recommended based on client's country of residence.
        /// </summary>
        [JsonProperty("recommended", NullValueHandling = NullValueHandling.Ignore)]
        public long? Recommended { get; set; }

        /// <summary>
        /// Account type supported by the server.
        /// </summary>
        [JsonProperty("supported_accounts", NullValueHandling = NullValueHandling.Ignore)]
        public string[] SupportedAccounts { get; set; }
    }

    /// <summary>
    /// Object containing geolocation information of the server.
    /// </summary>
    public partial class Geolocation
    {
        /// <summary>
        /// Geolocation country or place where server is located.
        /// </summary>
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public string Location { get; set; }

        /// <summary>
        /// Geolocation region where server is located.
        /// </summary>
        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }

        /// <summary>
        /// Sequence number of the server in that region.
        /// </summary>
        [JsonProperty("sequence", NullValueHandling = NullValueHandling.Ignore)]
        public long? Sequence { get; set; }
    }

    /// <summary>
    /// Current environment (installation instance) where servers are deployed. Currently, there
    /// are one demo and two real environments.
    /// </summary>
    public enum Environment { DerivDemo, DerivServer, DerivServer02 };

    /// <summary>
    /// Server unique id.
    /// </summary>
    public enum Id { P01Ts01, P01Ts02, P01Ts03, P01Ts04, P02Ts02 };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                EnvironmentConverter.Singleton,
                IdConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }


    public class EnvironmentConverter : JsonConverter
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
            }
            throw new Exception("Cannot marshal type Environment");
        }

        public static readonly EnvironmentConverter Singleton = new EnvironmentConverter();
    }

    internal class IdConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Id) || t == typeof(Id?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "p01_ts01":
                    return Id.P01Ts01;
                case "p01_ts02":
                    return Id.P01Ts02;
                case "p01_ts03":
                    return Id.P01Ts03;
                case "p01_ts04":
                    return Id.P01Ts04;
                case "p02_ts02":
                    return Id.P02Ts02;
            }
            throw new Exception("Cannot unmarshal type Id");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Id)untypedValue;
            switch (value)
            {
                case Id.P01Ts01:
                    serializer.Serialize(writer, "p01_ts01");
                    return;
                case Id.P01Ts02:
                    serializer.Serialize(writer, "p01_ts02");
                    return;
                case Id.P01Ts03:
                    serializer.Serialize(writer, "p01_ts03");
                    return;
                case Id.P01Ts04:
                    serializer.Serialize(writer, "p01_ts04");
                    return;
                case Id.P02Ts02:
                    serializer.Serialize(writer, "p02_ts02");
                    return;
            }
            throw new Exception("Cannot marshal type Id");
        }

        public static readonly IdConverter Singleton = new IdConverter();
    }
}
