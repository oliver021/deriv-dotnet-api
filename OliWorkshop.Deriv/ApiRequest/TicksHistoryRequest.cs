namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Get historic tick data for a given symbol.
    /// </summary>
    public partial class TicksHistoryRequest : TrackObject
    {
        /// <summary>
        /// [Optional] 1 - if the market is closed at the end time, or license limit is before end
        /// time, adjust interval backwards to compensate.
        /// </summary>
        [JsonProperty("adjust_start_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? AdjustStartTime { get; set; }

        /// <summary>
        /// [Optional] An upper limit on ticks to receive.
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }

        /// <summary>
        /// Epoch value representing the latest boundary of the returned ticks. If `latest` is
        /// specified, this will be the latest available timestamp.
        /// </summary>
        [JsonProperty("end")]
        public string End { get; set; }

        /// <summary>
        /// [Optional] Only applicable for style: `candles`. Candle time-dimension width setting.
        /// (default: `60`).
        /// </summary>
        [JsonProperty("granularity", NullValueHandling = NullValueHandling.Ignore)]
        public long Granularity { get; set; } = 60;

        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// [Optional] Epoch value representing the earliest boundary of the returned ticks.
        /// - For `"style": "ticks"`: this will default to 1 day ago.
        /// - For `"style": "candles"`: it will default to 1 day ago if count or granularity is
        /// undefined.
        /// </summary>
        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public long? Start { get; set; }

        /// <summary>
        /// [Optional] The tick-output style. for default is ticks
        /// </summary>
        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public Style Style { get; set; } = Style.Ticks;

        /// <summary>
        /// [Optional] 1 - to send updates whenever a new tick is received.
        /// </summary>
        [JsonProperty("subscribe", NullValueHandling = NullValueHandling.Ignore)]
        public long Subscribe { get; set; } = 0;

        /// <summary>
        /// Short symbol name (obtained from the `active_symbols` call).
        /// </summary>
        [JsonProperty("ticks_history")]
        public string TicksHistory { get; set; }
    }

    /// <summary>
    /// [Optional] The tick-output style.
    /// </summary>
    public enum Style { Candles, Ticks };

    /// <summary>
    /// The main class to contain setting of the tick history serializer
    /// </summary>
    public static class TickHistoryRequestConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                StyleConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public class StyleConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Style) || t == typeof(Style?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "candles":
                    return Style.Candles;
                case "ticks":
                    return Style.Ticks;
            }
            throw new Exception("Cannot unmarshal type Style");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Style)untypedValue;
            switch (value)
            {
                case Style.Candles:
                    serializer.Serialize(writer, "candles");
                    return;
                case Style.Ticks:
                    serializer.Serialize(writer, "ticks");
                    return;
            }
            throw new Exception("Cannot marshal type Style");
        }

        public static readonly StyleConverter Singleton = new StyleConverter();
    }
}
