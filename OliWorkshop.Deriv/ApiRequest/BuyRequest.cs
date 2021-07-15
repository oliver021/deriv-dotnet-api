namespace OliWorkshop.Deriv.ApiRequest
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Buy a Contract
    /// </summary>
    public class BuyRequest : TrackObject
    {
        /// <summary>
        /// Either the ID received from a Price Proposal (`proposal` call), or `1` if contract buy
        /// parameters are passed in the `parameters` field.
        /// </summary>
        [JsonProperty("buy")]
        public string Buy { get; set; }

        /// <summary>
        /// [Optional] Used to pass the parameters for contract buy.
        /// </summary>
        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public Parameters Parameters { get; set; }

        /// <summary>
        /// [Optional] Used to pass data through the websocket, which may be retrieved via the
        /// `echo_req` output field.
        /// </summary>
        [JsonProperty("passthrough", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Passthrough { get; set; }

        /// <summary>
        /// Maximum price at which to purchase the contract.
        /// </summary>
        [JsonProperty("price")]
        [JsonConverter(typeof(MinMaxValueCheckConverter))]
        public double Price { get; set; }

        /// <summary>
        /// [Optional] `1` to stream.
        /// </summary>
        [JsonProperty("subscribe", NullValueHandling = NullValueHandling.Ignore)]
        public long? Subscribe { get; set; }
    }

    /// <summary>
    /// [Optional] Used to pass the parameters for contract buy.
    /// </summary>
    public partial class Parameters
    {
        /// <summary>
        /// [Optional] Proposed payout or stake value
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(MinMaxValueCheckConverter))]
        public double Amount { get; set; }

        /// <summary>
        /// [Optional] Markup added to contract prices (as a percentage of contract payout)
        /// </summary>
        [JsonProperty("app_markup_percentage", NullValueHandling = NullValueHandling.Ignore)]
        public double? AppMarkupPercentage { get; set; }

        /// <summary>
        /// [Optional] Barrier for the contract (or last digit prediction for digit contracts).
        /// Contracts less than 24 hours in duration would need a relative barrier (barriers which
        /// need +/-), where entry spot would be adjusted accordingly with that amount to define a
        /// barrier, except for Synthetic Indices as they support both relative and absolute barriers.
        /// </summary>
        [JsonProperty("barrier", NullValueHandling = NullValueHandling.Ignore)]
        public string Barrier { get; set; }

        /// <summary>
        /// [Optional] Low barrier for the contract (for contracts with two barriers). Contracts less
        /// than 24 hours in duration would need a relative barrier (barriers which need +/-), where
        /// entry spot would be adjusted accordingly with that amount to define a barrier, except for
        /// Synthetic Indices as they support both relative and absolute barriers.
        /// </summary>
        [JsonProperty("barrier2", NullValueHandling = NullValueHandling.Ignore)]
        public string Barrier2 { get; set; } = string.Empty;

        /// <summary>
        /// [Optional] Indicates whether amount is 'payout' or 'stake' for binary options.
        /// </summary>
        [JsonProperty("basis", NullValueHandling = NullValueHandling.Ignore)]
        public Basis? Basis { get; set; }

        /// <summary>
        /// Cancellation duration option (only for `MULTUP` and `MULTDOWN` contracts).
        /// </summary>
        [JsonProperty("cancellation", NullValueHandling = NullValueHandling.Ignore)]
        public string Cancellation { get; set; }

        /// <summary>
        /// A valid contract-type
        /// </summary>
        [JsonProperty("contract_type")]
        public ContractType ContractType { get; set; }

        /// <summary>
        /// This can only be the account-holder's currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// [Optional] Epoch value of the expiry time of the contract. You must either specify
        /// date_expiry or duration.
        /// </summary>
        [JsonProperty("date_expiry", NullValueHandling = NullValueHandling.Ignore)]
        public long DateExpiry { get; set; }

        /// <summary>
        /// [Optional] For forward-starting contracts, epoch value of the starting time of the
        /// contract.
        /// </summary>
        [JsonProperty("date_start", NullValueHandling = NullValueHandling.Ignore)]
        public long? DateStart { get; set; }

        /// <summary>
        /// [Optional] Duration quantity
        /// </summary>
        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public long Duration { get; set; }

        /// <summary>
        /// [Optional] Duration unit is `s`: seconds, `m`: minutes, `h`: hours, `d`: days, `t`: ticks
        /// </summary>
        [JsonProperty("duration_unit", NullValueHandling = NullValueHandling.Ignore)]
        public DurationUnit DurationUnit { get; set; }

        /// <summary>
        /// Add an order to close the contract once the order condition is met (only for `MULTUP` and
        /// `MULTDOWN` contracts).
        /// </summary>
        [JsonProperty("limit_order", NullValueHandling = NullValueHandling.Ignore)]
        public LimitOrder LimitOrder { get; set; }

        /// <summary>
        /// [Optional] The multiplier for non-binary options. E.g. lookbacks.
        /// </summary>
        [JsonProperty("multiplier", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(MinMaxValueCheckConverter))]
        public double? Multiplier { get; set; }

        /// <summary>
        /// [Optional] The product type.
        /// </summary>
        [JsonProperty("product_type", NullValueHandling = NullValueHandling.Ignore)]
        public ProductType? ProductType { get; set; }

        /// <summary>
        /// [Optional] The tick that is predicted to have the highest/lowest value - for tickhigh and
        /// ticklow contracts.
        /// </summary>
        [JsonProperty("selected_tick", NullValueHandling = NullValueHandling.Ignore)]
        public long? SelectedTick { get; set; }

        /// <summary>
        /// Symbol code
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// [Optional] An epoch value of a predefined trading period start time
        /// </summary>
        [JsonProperty("trading_period_start", NullValueHandling = NullValueHandling.Ignore)]
        public long? TradingPeriodStart { get; set; }
    }

    /// <summary>
    /// Add an order to close the contract once the order condition is met (only for `MULTUP` and
    /// `MULTDOWN` contracts).
    /// </summary>
    public partial class LimitOrder
    {
        /// <summary>
        /// Contract will be automatically closed when the value of the contract reaches a specific
        /// loss.
        /// </summary>
        [JsonProperty("stop_loss", NullValueHandling = NullValueHandling.Ignore)]
        public double? StopLoss { get; set; }

        /// <summary>
        /// Contract will be automatically closed when the value of the contract reaches a specific
        /// profit.
        /// </summary>
        [JsonProperty("take_profit", NullValueHandling = NullValueHandling.Ignore)]
        public double? TakeProfit { get; set; }
    }

    /// <summary>
    /// [Optional] Indicates whether amount is 'payout' or 'stake' for binary options.
    /// </summary>
    public enum Basis { Payout, Stake };

    /// <summary>
    /// [Optional] Duration unit is `s`: seconds, `m`: minutes, `h`: hours, `d`: days, `t`: ticks
    /// </summary>
    public enum DurationUnit { Day, Hour, Minute, Second, Ticks };


    /// <summary>
    /// The type of basis in the purchase
    /// </summary>
    public class BasisConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Basis) || t == typeof(Basis?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "payout":
                    return Basis.Payout;
                case "stake":
                    return Basis.Stake;
            }
            throw new Exception("Cannot unmarshal type Basis");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Basis)untypedValue;
            switch (value)
            {
                case Basis.Payout:
                    serializer.Serialize(writer, "payout");
                    return;
                case Basis.Stake:
                    serializer.Serialize(writer, "stake");
                    return;
            }
            throw new Exception("Cannot marshal type Basis");
        }

        public static readonly BasisConverter Singleton = new BasisConverter();
    }

    public class ContractTypeConverter : JsonConverter
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

    public class DurationUnitConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DurationUnit) || t == typeof(DurationUnit?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "d":
                    return DurationUnit.Day;
                case "h":
                    return DurationUnit.Hour;
                case "m":
                    return DurationUnit.Minute;
                case "s":
                    return DurationUnit.Second;
                case "t":
                    return DurationUnit.Ticks;
            }
            throw new Exception("Cannot unmarshal type DurationUnit");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DurationUnit)untypedValue;
            switch (value)
            {
                case DurationUnit.Day:
                    serializer.Serialize(writer, "d");
                    return;
                case DurationUnit.Hour:
                    serializer.Serialize(writer, "h");
                    return;
                case DurationUnit.Minute:
                    serializer.Serialize(writer, "m");
                    return;
                case DurationUnit.Second:
                    serializer.Serialize(writer, "s");
                    return;
                case DurationUnit.Ticks:
                    serializer.Serialize(writer, "t");
                    return;
            }
            throw new Exception("Cannot marshal type DurationUnit");
        }

        public static readonly DurationUnitConverter Singleton = new DurationUnitConverter();
    }
}
