using Newtonsoft.Json;
using OliWorkshop.Topic.Deriv.ApiRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace OliWorkshop.Deriv.ApiRequest
{
    /// <summary>
    /// The reference to set serialize operations with objects
    /// </summary>
    public static class SerializerOptions
    {
        /// <summary>
        /// Property that represent the serializer setting to but contracts objects
        /// </summary>
        public static JsonSerializerSettings BuySerializer { get; } = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> {
                    new BasisConverter(),
                    new ContractTypeConverter(),
                    new DurationUnitConverter(),
                    new ProductTypeConverter()
                }
        };
    }
}
