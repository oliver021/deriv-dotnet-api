namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using OliWorkshop.Deriv.ApiRequests;

    /// <summary>
    /// For a given symbol, get the list of currently available contracts, and the latest barrier
    /// and duration limits for each contract.
    /// </summary>
    public partial class ContractForRequest
    {
        /// <summary>
        /// The short symbol name (obtained from `active_symbols` call).
        /// </summary>
        [JsonProperty("contracts_for")]
        public string ContractsFor { get; set; }

        /// <summary>
        /// [Optional] Currency of the contract's stake and payout (obtained from `payout_currencies`
        /// call).
        /// </summary>
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        /// <summary>
        /// [Optional] Indicates which landing company to get a list of contracts for. If you are
        /// logged in, your account's landing company will override this field.
        /// </summary>
        [JsonProperty("landing_company", NullValueHandling = NullValueHandling.Ignore)]
        public LandingCompany? LandingCompany { get; set; }

        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// [Optional] If you specify this field, only contracts tradable through that contract type
        /// will be returned.
        /// </summary>
        [JsonProperty("product_type", NullValueHandling = NullValueHandling.Ignore)]
        public ProductType? ProductType { get; set; }

        /// <summary>
        /// [Optional] Used to map request to response.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }
    }

   
    /// <summary>
    /// [Optional] If you specify this field, only contracts tradable through that contract type
    /// will be returned.
    /// </summary>
    public enum ProductType { Basic };

    public static class ContractForConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                LandingCompanyConverter.Singleton,
                ProductTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ProductTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ProductType) || t == typeof(ProductType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "basic")
            {
                return ProductType.Basic;
            }
            throw new Exception("Cannot unmarshal type ProductType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ProductType)untypedValue;
            if (value == ProductType.Basic)
            {
                serializer.Serialize(writer, "basic");
                return;
            }
            throw new Exception("Cannot marshal type ProductType");
        }

        public static readonly ProductTypeConverter Singleton = new ProductTypeConverter();
    }
}
