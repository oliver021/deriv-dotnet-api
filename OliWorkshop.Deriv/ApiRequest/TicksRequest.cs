namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Initiate a continuous stream of spot price updates for a given symbol.
    /// </summary>
    public partial class TicksRequest : TrackObject
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
        /// [Optional] If set to 1, will send updates whenever a new tick is received.
        /// </summary>
        [JsonProperty("subscribe", NullValueHandling = NullValueHandling.Ignore)]
        public long? Subscribe { get; set; }

        /// <summary>
        /// The short symbol name or array of symbols (obtained from `active_symbols` call).
        /// </summary>
        [JsonProperty("ticks")]
        public Ticks Ticks { get; set; }
    }

    /// <summary>
    /// The short symbol name or array of symbols (obtained from `active_symbols` call).
    /// </summary>
    public partial struct Ticks
    {
        public string String;
        public string[] StringArray;

        public static implicit operator Ticks(string String) => new Ticks { String = String };
        public static implicit operator Ticks(string[] StringArray) => new Ticks { StringArray = StringArray };
    }

    public class TicksRequestConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Ticks) || t == typeof(Ticks?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new Ticks { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<string[]>(reader);
                    return new Ticks { StringArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type Ticks");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Ticks)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.StringArray != null)
            {
                serializer.Serialize(writer, value.StringArray);
                return;
            }
            throw new Exception("Cannot marshal type Ticks");
        }

        public static readonly TicksRequestConverter Singleton = new TicksRequestConverter();
    }
}
