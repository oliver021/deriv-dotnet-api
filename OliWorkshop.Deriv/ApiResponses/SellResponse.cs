namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// A message with transaction results is received
    /// </summary>
    public partial class SellResponse
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
        /// Receipt for the transaction
        /// </summary>
        [JsonProperty("sell", NullValueHandling = NullValueHandling.Ignore)]
        public Sell Sell { get; set; }
    }

    /// <summary>
    /// Receipt for the transaction
    /// </summary>
    public partial class Sell
    {
        /// <summary>
        /// New account balance after completion of the sale
        /// </summary>
        [JsonProperty("balance_after", NullValueHandling = NullValueHandling.Ignore)]
        public double? BalanceAfter { get; set; }

        /// <summary>
        /// Internal contract identifier for the sold contract
        /// </summary>
        [JsonProperty("contract_id", NullValueHandling = NullValueHandling.Ignore)]
        public long ContractId { get; set; }

        /// <summary>
        /// Internal transaction identifier for the corresponding buy transaction
        /// </summary>
        [JsonProperty("reference_id", NullValueHandling = NullValueHandling.Ignore)]
        public long ReferenceId { get; set; }

        /// <summary>
        /// Actual effected sale price
        /// </summary>
        [JsonProperty("sold_for", NullValueHandling = NullValueHandling.Ignore)]
        public double SoldFor { get; set; }

        /// <summary>
        /// Internal transaction identifier for the sale transaction
        /// </summary>
        [JsonProperty("transaction_id", NullValueHandling = NullValueHandling.Ignore)]
        public long TransactionId { get; set; }
    }

    /// <summary>
    /// Action name of the request made.
    /// </summary>
    public enum MsgType { Sell };

    internal static class Converter
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

    internal class MsgTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MsgType) || t == typeof(MsgType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "sell")
            {
                return MsgType.Sell;
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
            var value = (MsgType)untypedValue;
            if (value == MsgType.Sell)
            {
                serializer.Serialize(writer, "sell");
                return;
            }
            throw new Exception("Cannot marshal type MsgType");
        }

        public static readonly MsgTypeConverter Singleton = new MsgTypeConverter();
    }
}
