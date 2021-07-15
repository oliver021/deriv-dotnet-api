namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Historic tick data for a single symbol
    /// </summary>
    public partial class TicksHistotyResponse
    {
        /// <summary>
        /// Array of OHLC (open/high/low/close) price values for the given time (only for
        /// style=`candles`)
        /// </summary>
        [JsonProperty("candles", NullValueHandling = NullValueHandling.Ignore)]
        public Candle[] Candles { get; set; }

        /// <summary>
        /// Echo of the request made.
        /// </summary>
        [JsonProperty("echo_req")]
        public Dictionary<string, object> EchoReq { get; set; }

        /// <summary>
        /// Historic tick data for a given symbol. Note: this will always return the latest possible
        /// set of ticks with accordance to the parameters specified.
        /// </summary>
        [JsonProperty("history", NullValueHandling = NullValueHandling.Ignore)]
        public History History { get; set; }

        /// <summary>
        /// Type of the response according to the `style` sent in request. Would be `history` or
        /// `candles` for the first response, and `tick` or `ohlc` for the rest when subscribed.
        /// </summary>
        [JsonProperty("msg_type")]
        public MsgTypeTickHistoryResponse MsgType { get; set; }

        /// <summary>
        /// Indicates the number of decimal points that the returned amounts must be displayed with
        /// </summary>
        [JsonProperty("pip_size", NullValueHandling = NullValueHandling.Ignore)]
        public double? PipSize { get; set; }

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

    public partial class Candle
    {
        /// <summary>
        /// It is the close price value for the given time
        /// </summary>
        [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
        public double? Close { get; set; }

        /// <summary>
        /// It is an epoch value
        /// </summary>
        [JsonProperty("epoch", NullValueHandling = NullValueHandling.Ignore)]
        public long? Epoch { get; set; }

        /// <summary>
        /// It is the high price value for the given time
        /// </summary>
        [JsonProperty("high", NullValueHandling = NullValueHandling.Ignore)]
        public double? High { get; set; }

        /// <summary>
        /// It is the low price value for the given time
        /// </summary>
        [JsonProperty("low", NullValueHandling = NullValueHandling.Ignore)]
        public double? Low { get; set; }

        /// <summary>
        /// It is the open price value for the given time
        /// </summary>
        [JsonProperty("open", NullValueHandling = NullValueHandling.Ignore)]
        public double? Open { get; set; }
    }

    /// <summary>
    /// Historic tick data for a given symbol. Note: this will always return the latest possible
    /// set of ticks with accordance to the parameters specified.
    /// </summary>
    public partial class History
    {
        /// <summary>
        /// An array containing list of tick values for the corresponding epoch values in `times`
        /// array.
        /// </summary>
        [JsonProperty("prices", NullValueHandling = NullValueHandling.Ignore)]
        public double[] Prices { get; set; }

        /// <summary>
        /// An array containing list of epoch values for the corresponding tick values in `prices`
        /// array.
        /// </summary>
        [JsonProperty("times", NullValueHandling = NullValueHandling.Ignore)]
        public long[] Times { get; set; }
    }

    /// <summary>
    /// Type of the response according to the `style` sent in request. Would be `history` or
    /// `candles` for the first response, and `tick` or `ohlc` for the rest when subscribed.
    /// </summary>
    public enum MsgTypeTickHistoryResponse { Candles, History, Ohlc, Tick };

    internal static class ConverterTickHistoryResponse
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                MsgTypeConverterTickHistoryResponse.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class MsgTypeConverterTickHistoryResponse : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MsgTypeTickHistoryResponse) || t == typeof(MsgTypeTickHistoryResponse?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "candles":
                    return MsgTypeTickHistoryResponse.Candles;
                case "history":
                    return MsgTypeTickHistoryResponse.History;
                case "ohlc":
                    return MsgTypeTickHistoryResponse.Ohlc;
                case "tick":
                    return MsgTypeTickHistoryResponse.Tick;
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
            var value = (MsgTypeTickHistoryResponse)untypedValue;
            switch (value)
            {
                case MsgTypeTickHistoryResponse.Candles:
                    serializer.Serialize(writer, "candles");
                    return;
                case MsgTypeTickHistoryResponse.History:
                    serializer.Serialize(writer, "history");
                    return;
                case MsgTypeTickHistoryResponse.Ohlc:
                    serializer.Serialize(writer, "ohlc");
                    return;
                case MsgTypeTickHistoryResponse.Tick:
                    serializer.Serialize(writer, "tick");
                    return;
            }
            throw new Exception("Cannot marshal type MsgType");
        }

        public static readonly MsgTypeConverter Singleton = new MsgTypeConverter();
    }
}
