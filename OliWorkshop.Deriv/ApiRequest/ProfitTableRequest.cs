namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Retrieve a summary of account Profit Table, according to given search criteria
    /// </summary>
    public partial class ProfitTableRequest : TrackObject
    {
        /// <summary>
        /// Return only contracts of the specified types
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public ContractType[] ContractType { get; set; }

        /// <summary>
        /// [Optional] Start date (epoch or YYYY-MM-DD)
        /// </summary>
        [JsonProperty("date_from", NullValueHandling = NullValueHandling.Ignore)]
        public string DateFrom { get; set; }

        /// <summary>
        /// [Optional] End date (epoch or YYYY-MM-DD)
        /// </summary>
        [JsonProperty("date_to", NullValueHandling = NullValueHandling.Ignore)]
        public string DateTo { get; set; }

        /// <summary>
        /// [Optional] If set to 1, will return full contracts description.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public long? Description { get; set; }

        /// <summary>
        /// [Optional] Apply upper limit to count of transactions received.
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(MinMaxValueCheckConverter))]
        public double? Limit { get; set; }

        /// <summary>
        /// [Optional] Number of transactions to skip.
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public double? Offset { get; set; }

        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// Must be `1`
        /// </summary>
        [JsonProperty("profit_table")]
        public long ProfitTable { get; set; } = 1;

        /// <summary>
        /// [Optional] Sort direction.
        /// </summary>
        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public Sort? Sort { get; set; }
    }

    /// <summary>
    /// The main setting to contains converters to profit tables
    /// </summary>
    public static class ConverterProfitTable
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ContractTypeConverter.Singleton,
                SortConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ContractTypeConverterProfitTable : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ContractType) || t == typeof(ContractType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "ASIAND":
                    return ContractType.Asiand;
                case "ASIANU":
                    return ContractType.Asianu;
                case "CALL":
                    return ContractType.Call;
                case "CALLE":
                    return ContractType.Calle;
                case "CALLSPREAD":
                    return ContractType.Callspread;
                case "DIGITDIFF":
                    return ContractType.Digitdiff;
                case "DIGITEVEN":
                    return ContractType.Digiteven;
                case "DIGITMATCH":
                    return ContractType.Digitmatch;
                case "DIGITODD":
                    return ContractType.Digitodd;
                case "DIGITOVER":
                    return ContractType.Digitover;
                case "DIGITUNDER":
                    return ContractType.Digitunder;
                case "EXPIRYMISS":
                    return ContractType.Expirymiss;
                case "EXPIRYMISSE":
                    return ContractType.Expirymisse;
                case "EXPIRYRANGE":
                    return ContractType.Expiryrange;
                case "EXPIRYRANGEE":
                    return ContractType.Expiryrangee;
                case "LBFLOATCALL":
                    return ContractType.Lbfloatcall;
                case "LBFLOATPUT":
                    return ContractType.Lbfloatput;
                case "LBHIGHLOW":
                    return ContractType.Lbhighlow;
                case "MULTDOWN":
                    return ContractType.Multdown;
                case "MULTUP":
                    return ContractType.Multup;
                case "NOTOUCH":
                    return ContractType.Notouch;
                case "ONETOUCH":
                    return ContractType.Onetouch;
                case "PUT":
                    return ContractType.Put;
                case "PUTE":
                    return ContractType.Pute;
                case "PUTSPREAD":
                    return ContractType.Putspread;
                case "RANGE":
                    return ContractType.Range;
                case "RESETCALL":
                    return ContractType.Resetcall;
                case "RESETPUT":
                    return ContractType.Resetput;
                case "RUNHIGH":
                    return ContractType.Runhigh;
                case "RUNLOW":
                    return ContractType.Runlow;
                case "TICKHIGH":
                    return ContractType.Tickhigh;
                case "TICKLOW":
                    return ContractType.Ticklow;
                case "UPORDOWN":
                    return ContractType.Upordown;
            }
            throw new Exception("Cannot unmarshal type ContractType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ContractType)untypedValue;
            switch (value)
            {
                case ContractType.Asiand:
                    serializer.Serialize(writer, "ASIAND");
                    return;
                case ContractType.Asianu:
                    serializer.Serialize(writer, "ASIANU");
                    return;
                case ContractType.Call:
                    serializer.Serialize(writer, "CALL");
                    return;
                case ContractType.Calle:
                    serializer.Serialize(writer, "CALLE");
                    return;
                case ContractType.Callspread:
                    serializer.Serialize(writer, "CALLSPREAD");
                    return;
                case ContractType.Digitdiff:
                    serializer.Serialize(writer, "DIGITDIFF");
                    return;
                case ContractType.Digiteven:
                    serializer.Serialize(writer, "DIGITEVEN");
                    return;
                case ContractType.Digitmatch:
                    serializer.Serialize(writer, "DIGITMATCH");
                    return;
                case ContractType.Digitodd:
                    serializer.Serialize(writer, "DIGITODD");
                    return;
                case ContractType.Digitover:
                    serializer.Serialize(writer, "DIGITOVER");
                    return;
                case ContractType.Digitunder:
                    serializer.Serialize(writer, "DIGITUNDER");
                    return;
                case ContractType.Expirymiss:
                    serializer.Serialize(writer, "EXPIRYMISS");
                    return;
                case ContractType.Expirymisse:
                    serializer.Serialize(writer, "EXPIRYMISSE");
                    return;
                case ContractType.Expiryrange:
                    serializer.Serialize(writer, "EXPIRYRANGE");
                    return;
                case ContractType.Expiryrangee:
                    serializer.Serialize(writer, "EXPIRYRANGEE");
                    return;
                case ContractType.Lbfloatcall:
                    serializer.Serialize(writer, "LBFLOATCALL");
                    return;
                case ContractType.Lbfloatput:
                    serializer.Serialize(writer, "LBFLOATPUT");
                    return;
                case ContractType.Lbhighlow:
                    serializer.Serialize(writer, "LBHIGHLOW");
                    return;
                case ContractType.Multdown:
                    serializer.Serialize(writer, "MULTDOWN");
                    return;
                case ContractType.Multup:
                    serializer.Serialize(writer, "MULTUP");
                    return;
                case ContractType.Notouch:
                    serializer.Serialize(writer, "NOTOUCH");
                    return;
                case ContractType.Onetouch:
                    serializer.Serialize(writer, "ONETOUCH");
                    return;
                case ContractType.Put:
                    serializer.Serialize(writer, "PUT");
                    return;
                case ContractType.Pute:
                    serializer.Serialize(writer, "PUTE");
                    return;
                case ContractType.Putspread:
                    serializer.Serialize(writer, "PUTSPREAD");
                    return;
                case ContractType.Range:
                    serializer.Serialize(writer, "RANGE");
                    return;
                case ContractType.Resetcall:
                    serializer.Serialize(writer, "RESETCALL");
                    return;
                case ContractType.Resetput:
                    serializer.Serialize(writer, "RESETPUT");
                    return;
                case ContractType.Runhigh:
                    serializer.Serialize(writer, "RUNHIGH");
                    return;
                case ContractType.Runlow:
                    serializer.Serialize(writer, "RUNLOW");
                    return;
                case ContractType.Tickhigh:
                    serializer.Serialize(writer, "TICKHIGH");
                    return;
                case ContractType.Ticklow:
                    serializer.Serialize(writer, "TICKLOW");
                    return;
                case ContractType.Upordown:
                    serializer.Serialize(writer, "UPORDOWN");
                    return;
            }
            throw new Exception("Cannot marshal type ContractType");
        }

        public static readonly ContractTypeConverter Singleton = new ContractTypeConverter();
    }

    public class SortConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Sort) || t == typeof(Sort?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "ASC":
                    return Sort.Asc;
                case "DESC":
                    return Sort.Desc;
            }
            throw new Exception("Cannot unmarshal type Sort");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Sort)untypedValue;
            switch (value)
            {
                case Sort.Asc:
                    serializer.Serialize(writer, "ASC");
                    return;
                case Sort.Desc:
                    serializer.Serialize(writer, "DESC");
                    return;
            }
            throw new Exception("Cannot marshal type Sort");
        }

        public static readonly SortConverter Singleton = new SortConverter();
    }
}

