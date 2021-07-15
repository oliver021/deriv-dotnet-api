namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Retrieve a summary of account transactions, according to given search criteria
    /// </summary>
    public partial class StatementRequest : TrackObject
    {
        /// <summary>
        /// [Optional] To filter the statement according to the type of transaction.
        /// </summary>
        [JsonProperty("action_type", NullValueHandling = NullValueHandling.Ignore)]
        public ActionType? ActionType { get; set; }

        /// <summary>
        /// [Optional] Start date (epoch)
        /// </summary>
        [JsonProperty("date_from", NullValueHandling = NullValueHandling.Ignore)]
        public long? DateFrom { get; set; }

        /// <summary>
        /// [Optional] End date (epoch)
        /// </summary>
        [JsonProperty("date_to", NullValueHandling = NullValueHandling.Ignore)]
        public long? DateTo { get; set; }

        /// <summary>
        /// [Optional] If set to 1, will return full contracts description.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public long? Description { get; set; }

        /// <summary>
        /// [Optional] Maximum number of transactions to receive.
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(MinMaxValueCheckConverter))]
        public long? Limit { get; set; }

        /// <summary>
        /// [Optional] Number of transactions to skip.
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public long? Offset { get; set; }

        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// Must be `1`
        /// </summary>
        [JsonProperty("statement")]
        public long Statement { get; set; } = 1;
    }

    /// <summary>
    /// Basic statement converter preset
    /// </summary>
    public static class StatementConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ActionTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ActionTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ActionType) || t == typeof(ActionType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "adjustment":
                    return ActionType.Adjustment;
                case "buy":
                    return ActionType.Buy;
                case "deposit":
                    return ActionType.Deposit;
                case "escrow":
                    return ActionType.Escrow;
                case "sell":
                    return ActionType.Sell;
                case "transfer":
                    return ActionType.Transfer;
                case "virtual_credit":
                    return ActionType.VirtualCredit;
                case "withdrawal":
                    return ActionType.Withdrawal;
            }
            throw new Exception("Cannot unmarshal type ActionType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ActionType)untypedValue;
            switch (value)
            {
                case ActionType.Adjustment:
                    serializer.Serialize(writer, "adjustment");
                    return;
                case ActionType.Buy:
                    serializer.Serialize(writer, "buy");
                    return;
                case ActionType.Deposit:
                    serializer.Serialize(writer, "deposit");
                    return;
                case ActionType.Escrow:
                    serializer.Serialize(writer, "escrow");
                    return;
                case ActionType.Sell:
                    serializer.Serialize(writer, "sell");
                    return;
                case ActionType.Transfer:
                    serializer.Serialize(writer, "transfer");
                    return;
                case ActionType.VirtualCredit:
                    serializer.Serialize(writer, "virtual_credit");
                    return;
                case ActionType.Withdrawal:
                    serializer.Serialize(writer, "withdrawal");
                    return;
            }
            throw new Exception("Cannot marshal type ActionType");
        }

        public static readonly ActionTypeConverter Singleton = new ActionTypeConverter();
    }

    internal class MinMaxValueCheckConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(double) || t == typeof(double?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<double>(reader);
            if (value >= 0 && value <= 999)
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
            if (value >= 0 && value <= 999)
            {
                serializer.Serialize(writer, value);
                return;
            }
            throw new Exception("Cannot marshal type double");
        }

        public static readonly MinMaxValueCheckConverter Singleton = new MinMaxValueCheckConverter();
    }
}
