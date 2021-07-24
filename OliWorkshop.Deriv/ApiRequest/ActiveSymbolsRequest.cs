namespace OliWorkshop.Deriv.ApiRequests
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Retrieve a list of all currently active symbols (underlying markets upon which contracts
    /// are available for trading).
    /// </summary>
    public partial class ActiveSymbolsRequest : TrackObject
    {
        /// <summary>
        /// If you use `brief`, only a subset of fields will be returned.
        /// </summary>
        [JsonProperty("active_symbols")]
        public ActiveSymbols ActiveSymbols { get; set; }

        /// <summary>
        /// [Optional] If you specify this field, only symbols available for trading by that landing
        /// company will be returned. If you are logged in, only symbols available for trading by
        /// your landing company will be returned regardless of what you specify in this field.
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
        /// [Optional] If you specify this field, only symbols that can be traded through that
        /// product type will be returned.
        /// </summary>
        [JsonProperty("product_type", NullValueHandling = NullValueHandling.Ignore)]
        public ProductType? ProductType { get; set; }
    }

    /// <summary>
    /// If you use `brief`, only a subset of fields will be returned.
    /// </summary>
    public enum ActiveSymbols { Brief, Full };

    /// <summary>
    /// [Optional] If you specify this field, only symbols that can be traded through that
    /// product type will be returned.
    /// </summary>
    public enum ProductType { Basic };

    internal class ActiveSymbolsConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ActiveSymbols) || t == typeof(ActiveSymbols?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "brief":
                    return ActiveSymbols.Brief;
                case "full":
                    return ActiveSymbols.Full;
            }
            throw new Exception("Cannot unmarshal type ActiveSymbols");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ActiveSymbols)untypedValue;
            switch (value)
            {
                case ActiveSymbols.Brief:
                    serializer.Serialize(writer, "brief");
                    return;
                case ActiveSymbols.Full:
                    serializer.Serialize(writer, "full");
                    return;
            }
            throw new Exception("Cannot marshal type ActiveSymbols");
        }

        public static readonly ActiveSymbolsConverter Singleton = new ActiveSymbolsConverter();
    }

    public class ProductTypeConverter : JsonConverter
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
