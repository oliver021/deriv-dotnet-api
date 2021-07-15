namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Latest price and other details for an open contract in the user's portfolio
    /// </summary>
    public partial class ProposalOpenResponse
    {
        /// <summary>
        /// Echo of the request made.
        /// </summary>
        [JsonProperty("echo_req")]
        public Dictionary<string, object> EchoReq { get; set; }

        /// <summary>
        /// Action name of the request made.
        /// </summary>
        [JsonProperty("msg_type", NullValueHandling = NullValueHandling.Ignore)]
        public MsgType MsgType { get; set; }

        /// <summary>
        /// Latest price and other details for an open contract
        /// </summary>
        [JsonProperty("proposal_open_contract", NullValueHandling = NullValueHandling.Ignore)]
        public ProposalOpenContract ProposalOpenContract { get; set; }

        /// <summary>
        /// Optional field sent in request to map to response, present only when request contains
        /// `req_id`.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long ReqId { get; set; }

        /// <summary>
        /// For subscription requests only.
        /// </summary>
        [JsonProperty("subscription", NullValueHandling = NullValueHandling.Ignore)]
        public SubscriptionInformation Subscription { get; set; }
    }

    /// <summary>
    /// Latest price and other details for an open contract
    /// </summary>
    public partial class ProposalOpenContract
    {
        /// <summary>
        /// Tick details around contract start and end time.
        /// </summary>
        [JsonProperty("audit_details")]
        public AuditDetailsForExpiredContract AuditDetails { get; set; }

        /// <summary>
        /// Barrier of the contract (if any).
        /// </summary>
        [JsonProperty("barrier")]
        public string Barrier { get; set; }

        /// <summary>
        /// The number of barriers a contract has.
        /// </summary>
        [JsonProperty("barrier_count", NullValueHandling = NullValueHandling.Ignore)]
        public double BarrierCount { get; set; }

        /// <summary>
        /// Price at which the contract could be sold back to the company.
        /// </summary>
        [JsonProperty("bid_price", NullValueHandling = NullValueHandling.Ignore)]
        public double BidPrice { get; set; }

        /// <summary>
        /// Price at which contract was purchased
        /// </summary>
        [JsonProperty("buy_price", NullValueHandling = NullValueHandling.Ignore)]
        public double BuyPrice { get; set; }

        /// <summary>
        /// Contains information about contract cancellation option.
        /// </summary>
        [JsonProperty("cancellation", NullValueHandling = NullValueHandling.Ignore)]
        public Cancellation Cancellation { get; set; }

        /// <summary>
        /// Commission in payout currency amount.
        /// </summary>
        [JsonProperty("commision")]
        public double Commision { get; set; }

        /// <summary>
        /// The internal contract identifier
        /// </summary>
        [JsonProperty("contract_id", NullValueHandling = NullValueHandling.Ignore)]
        public long ContractId { get; set; }

        /// <summary>
        /// Contract type.
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractType { get; set; }

        /// <summary>
        /// The currency code of the contract.
        /// </summary>
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        /// <summary>
        /// Spot value if we have license to stream this symbol.
        /// </summary>
        [JsonProperty("current_spot", NullValueHandling = NullValueHandling.Ignore)]
        public double CurrentSpot { get; set; }

        /// <summary>
        /// Spot value with the correct precision if we have license to stream this symbol.
        /// </summary>
        [JsonProperty("current_spot_display_value", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrentSpotDisplayValue { get; set; }

        /// <summary>
        /// The corresponding time of the current spot.
        /// </summary>
        [JsonProperty("current_spot_time", NullValueHandling = NullValueHandling.Ignore)]
        public long CurrentSpotTime { get; set; }

        /// <summary>
        /// Expiry date (epoch) of the Contract. Please note that it is not applicable for tick trade
        /// contracts.
        /// </summary>
        [JsonProperty("date_expiry", NullValueHandling = NullValueHandling.Ignore)]
        public long DateExpiry { get; set; }

        /// <summary>
        /// Settlement date (epoch) of the contract.
        /// </summary>
        [JsonProperty("date_settlement", NullValueHandling = NullValueHandling.Ignore)]
        public long DateSettlement { get; set; }

        /// <summary>
        /// Start date (epoch) of the contract.
        /// </summary>
        [JsonProperty("date_start", NullValueHandling = NullValueHandling.Ignore)]
        public long DateStart { get; set; }

        /// <summary>
        /// Display name of underlying
        /// </summary>
        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The `bid_price` with the correct precision
        /// </summary>
        [JsonProperty("display_value", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayValue { get; set; }

        /// <summary>
        /// Same as `entry_tick`. For backwards compatibility.
        /// </summary>
        [JsonProperty("entry_spot")]
        public double EntrySpot { get; set; }

        /// <summary>
        /// Same as `entry_tick_display_value`. For backwards compatibility.
        /// </summary>
        [JsonProperty("entry_spot_display_value")]
        public string EntrySpotDisplayValue { get; set; }

        /// <summary>
        /// This is the entry spot of the contract. For contracts starting immediately it is the next
        /// tick after the start time. For forward-starting contracts it is the spot at the start
        /// time.
        /// </summary>
        [JsonProperty("entry_tick", NullValueHandling = NullValueHandling.Ignore)]
        public double EntryTick { get; set; }

        /// <summary>
        /// This is the entry spot with the correct precision of the contract. For contracts starting
        /// immediately it is the next tick after the start time. For forward-starting contracts it
        /// is the spot at the start time.
        /// </summary>
        [JsonProperty("entry_tick_display_value", NullValueHandling = NullValueHandling.Ignore)]
        public string EntryTickDisplayValue { get; set; }

        /// <summary>
        /// This is the epoch time of the entry tick.
        /// </summary>
        [JsonProperty("entry_tick_time", NullValueHandling = NullValueHandling.Ignore)]
        public long EntryTickTime { get; set; }

        /// <summary>
        /// Exit tick can refer to the latest tick at the end time, the tick that fulfils the
        /// contract's winning or losing condition for path dependent contracts (Touch/No Touch and
        /// Stays Between/Goes Outside) or the tick at which the contract is sold before expiry.
        /// </summary>
        [JsonProperty("exit_tick", NullValueHandling = NullValueHandling.Ignore)]
        public double ExitTick { get; set; }

        /// <summary>
        /// Exit tick can refer to the latest tick at the end time, the tick that fulfils the
        /// contract's winning or losing condition for path dependent contracts (Touch/No Touch and
        /// Stays Between/Goes Outside) or the tick at which the contract is sold before expiry.
        /// </summary>
        [JsonProperty("exit_tick_display_value", NullValueHandling = NullValueHandling.Ignore)]
        public string ExitTickDisplayValue { get; set; }

        /// <summary>
        /// This is the epoch time of the exit tick. Note that since certain instruments don't tick
        /// every second, the exit tick time may be a few seconds before the end time.
        /// </summary>
        [JsonProperty("exit_tick_time", NullValueHandling = NullValueHandling.Ignore)]
        public long ExitTickTime { get; set; }

        /// <summary>
        /// High barrier of the contract (if any).
        /// </summary>
        [JsonProperty("high_barrier", NullValueHandling = NullValueHandling.Ignore)]
        public string HighBarrier { get; set; }

        /// <summary>
        /// A per-connection unique identifier. Can be passed to the `forget` API call to unsubscribe.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// Whether the contract is expired or not.
        /// </summary>
        [JsonProperty("is_expired", NullValueHandling = NullValueHandling.Ignore)]
        public long IsExpired { get; set; }

        /// <summary>
        /// Whether the contract is forward-starting or not.
        /// </summary>
        [JsonProperty("is_forward_starting", NullValueHandling = NullValueHandling.Ignore)]
        public long IsForwardStarting { get; set; }

        /// <summary>
        /// Whether the contract is an intraday contract.
        /// </summary>
        [JsonProperty("is_intraday", NullValueHandling = NullValueHandling.Ignore)]
        public long IsIntraday { get; set; }

        /// <summary>
        /// Whether the contract expiry price will depend on the path of the market (e.g. One Touch
        /// contract).
        /// </summary>
        [JsonProperty("is_path_dependent", NullValueHandling = NullValueHandling.Ignore)]
        public long IsPathDependent { get; set; }

        /// <summary>
        /// Whether the contract is settleable or not.
        /// </summary>
        [JsonProperty("is_settleable", NullValueHandling = NullValueHandling.Ignore)]
        public long IsSettleable { get; set; }

        /// <summary>
        /// Whether the contract is sold or not.
        /// </summary>
        [JsonProperty("is_sold", NullValueHandling = NullValueHandling.Ignore)]
        public long IsSold { get; set; }

        /// <summary>
        /// Whether the contract can be cancelled.
        /// </summary>
        [JsonProperty("is_valid_to_cancel", NullValueHandling = NullValueHandling.Ignore)]
        public long IsValidToCancel { get; set; }

        /// <summary>
        /// Whether the contract can be sold back to the company.
        /// </summary>
        [JsonProperty("is_valid_to_sell", NullValueHandling = NullValueHandling.Ignore)]
        public long IsValidToSell { get; set; }

        /// <summary>
        /// Orders are applicable to `MULTUP` and `MULTDOWN` contracts only.
        /// </summary>
        [JsonProperty("limit_order", NullValueHandling = NullValueHandling.Ignore)]
        public LimitOrder LimitOrder { get; set; }

        /// <summary>
        /// Text description of the contract purchased, Example: Win payout if Volatility 100 Index
        /// is strictly higher than entry spot at 10 minutes after contract start time.
        /// </summary>
        [JsonProperty("longcode", NullValueHandling = NullValueHandling.Ignore)]
        public string Longcode { get; set; }

        /// <summary>
        /// Low barrier of the contract (if any).
        /// </summary>
        [JsonProperty("low_barrier", NullValueHandling = NullValueHandling.Ignore)]
        public string LowBarrier { get; set; }

        /// <summary>
        /// [Only for lookback trades] Multiplier applies when calculating the final payoff for each
        /// type of lookback. e.g. (Exit spot - Lowest historical price) * multiplier = Payout
        /// </summary>
        [JsonProperty("multiplier", NullValueHandling = NullValueHandling.Ignore)]
        public double Multiplier { get; set; }

        /// <summary>
        /// Payout value of the contract.
        /// </summary>
        [JsonProperty("payout", NullValueHandling = NullValueHandling.Ignore)]
        public double Payout { get; set; }

        /// <summary>
        /// The latest bid price minus buy price.
        /// </summary>
        [JsonProperty("profit", NullValueHandling = NullValueHandling.Ignore)]
        public double Profit { get; set; }

        /// <summary>
        /// Profit in percentage.
        /// </summary>
        [JsonProperty("profit_percentage", NullValueHandling = NullValueHandling.Ignore)]
        public double ProfitPercentage { get; set; }

        /// <summary>
        /// Epoch of purchase time, will be same as `date_start` for all contracts except forward
        /// starting contracts.
        /// </summary>
        [JsonProperty("purchase_time", NullValueHandling = NullValueHandling.Ignore)]
        public long PurchaseTime { get; set; }

        /// <summary>
        /// [Only for reset trades] The epoch time of a barrier reset.
        /// </summary>
        [JsonProperty("reset_time", NullValueHandling = NullValueHandling.Ignore)]
        public long ResetTime { get; set; }

        /// <summary>
        /// Price at which contract was sold, only available when contract has been sold.
        /// </summary>
        [JsonProperty("sell_price", NullValueHandling = NullValueHandling.Ignore)]
        public double SellPrice { get; set; }

        /// <summary>
        /// Latest spot value at the sell time. (only present for contracts already sold). Will no
        /// longer be supported in the next API release.
        /// </summary>
        [JsonProperty("sell_spot", NullValueHandling = NullValueHandling.Ignore)]
        public double SellSpot { get; set; }

        /// <summary>
        /// Latest spot value with the correct precision at the sell time. (only present for
        /// contracts already sold). Will no longer be supported in the next API release.
        /// </summary>
        [JsonProperty("sell_spot_display_value", NullValueHandling = NullValueHandling.Ignore)]
        public string SellSpotDisplayValue { get; set; }

        /// <summary>
        /// Epoch time of the sell spot. Note that since certain underlyings don't tick every second,
        /// the sell spot time may be a few seconds before the sell time. (only present for contracts
        /// already sold). Will no longer be supported in the next API release.
        /// </summary>
        [JsonProperty("sell_spot_time", NullValueHandling = NullValueHandling.Ignore)]
        public long SellSpotTime { get; set; }

        /// <summary>
        /// Epoch time of when the contract was sold (only present for contracts already sold)
        /// </summary>
        [JsonProperty("sell_time")]
        public long SellTime { get; set; }

        /// <summary>
        /// Coded description of the contract purchased.
        /// </summary>
        [JsonProperty("shortcode", NullValueHandling = NullValueHandling.Ignore)]
        public string Shortcode { get; set; }

        /// <summary>
        /// Contract status. Will be `sold` if the contract was sold back before expiry, `won` if won
        /// and `lost` if lost at expiry. Otherwise will be `open`
        /// </summary>
        [JsonProperty("status")]
        public ContractStatus Status { get; set; }

        /// <summary>
        /// Only for tick trades, number of ticks
        /// </summary>
        [JsonProperty("tick_count", NullValueHandling = NullValueHandling.Ignore)]
        public long TickCount { get; set; }

        /// <summary>
        /// Tick stream from entry to end time.
        /// </summary>
        [JsonProperty("tick_stream", NullValueHandling = NullValueHandling.Ignore)]
        public TickStream[] TickStream { get; set; }

        /// <summary>
        /// Every contract has buy and sell transaction ids, i.e. when you purchase a contract we
        /// associate it with buy transaction id, and if contract is already sold we associate that
        /// with sell transaction id.
        /// </summary>
        [JsonProperty("transaction_ids", NullValueHandling = NullValueHandling.Ignore)]
        public TransactionIdsForContract TransactionIds { get; set; }

        /// <summary>
        /// The underlying symbol code.
        /// </summary>
        [JsonProperty("underlying", NullValueHandling = NullValueHandling.Ignore)]
        public string Underlying { get; set; }

        /// <summary>
        /// Error message if validation fails
        /// </summary>
        [JsonProperty("validation_error", NullValueHandling = NullValueHandling.Ignore)]
        public string ValidationError { get; set; }
    }

    public partial class AuditDetailsForExpiredContract
    {
        /// <summary>
        /// Ticks for tick expiry contract from start time till expiry.
        /// </summary>
        [JsonProperty("all_ticks", NullValueHandling = NullValueHandling.Ignore)]
        public AllTick[] AllTicks { get; set; }

        /// <summary>
        /// Ticks around contract end time.
        /// </summary>
        [JsonProperty("contract_end", NullValueHandling = NullValueHandling.Ignore)]
        public ContractEnd[] ContractEnd { get; set; }

        /// <summary>
        /// Ticks around contract start time.
        /// </summary>
        [JsonProperty("contract_start", NullValueHandling = NullValueHandling.Ignore)]
        public ContractStart[] ContractStart { get; set; }
    }

    public partial class AllTick
    {
        /// <summary>
        /// Epoch time of a tick or the contract start or end time.
        /// </summary>
        [JsonProperty("epoch", NullValueHandling = NullValueHandling.Ignore)]
        public long Epoch { get; set; }

        /// <summary>
        /// A flag used to highlight the record in front-end applications.
        /// </summary>
        [JsonProperty("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// A short description of the data. It could be a tick or a time associated with the
        /// contract.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The spot value at the given epoch.
        /// </summary>
        [JsonProperty("tick")]
        public double Tick { get; set; }

        /// <summary>
        /// The spot value with the correct precision at the given epoch.
        /// </summary>
        [JsonProperty("tick_display_value")]
        public string TickDisplayValue { get; set; }
    }

    public partial class ContractEnd
    {
        /// <summary>
        /// Epoch time of a tick or the contract start or end time.
        /// </summary>
        [JsonProperty("epoch", NullValueHandling = NullValueHandling.Ignore)]
        public long Epoch { get; set; }

        /// <summary>
        /// A flag used to highlight the record in front-end applications.
        /// </summary>
        [JsonProperty("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// A short description of the data. It could be a tick or a time associated with the
        /// contract.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The spot value at the given epoch.
        /// </summary>
        [JsonProperty("tick")]
        public double Tick { get; set; }

        /// <summary>
        /// The spot value with the correct precision at the given epoch.
        /// </summary>
        [JsonProperty("tick_display_value")]
        public string TickDisplayValue { get; set; }
    }

    public partial class ContractStart
    {
        /// <summary>
        /// Epoch time of a tick or the contract start or end time.
        /// </summary>
        [JsonProperty("epoch", NullValueHandling = NullValueHandling.Ignore)]
        public long Epoch { get; set; }

        /// <summary>
        /// A flag used to highlight the record in front-end applications.
        /// </summary>
        [JsonProperty("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// A short description of the data. It could be a tick or a time associated with the
        /// contract.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The spot value at the given epoch.
        /// </summary>
        [JsonProperty("tick")]
        public double Tick { get; set; }

        /// <summary>
        /// The spot value with the correct precision at the given epoch.
        /// </summary>
        [JsonProperty("tick_display_value")]
        public string TickDisplayValue { get; set; }
    }

    /// <summary>
    /// Contains information about contract cancellation option.
    /// </summary>
    public partial class Cancellation
    {
        /// <summary>
        /// Ask price of contract cancellation option.
        /// </summary>
        [JsonProperty("ask_price", NullValueHandling = NullValueHandling.Ignore)]
        public double AskPrice { get; set; }

        /// <summary>
        /// Expiry time in epoch for contract cancellation option.
        /// </summary>
        [JsonProperty("date_expiry", NullValueHandling = NullValueHandling.Ignore)]
        public long DateExpiry { get; set; }
    }

    /// <summary>
    /// Orders are applicable to `MULTUP` and `MULTDOWN` contracts only.
    /// </summary>
    public partial class LimitOrder
    {
        /// <summary>
        /// Contains information where the contract will be closed automatically at the loss
        /// specified by the user.
        /// </summary>
        [JsonProperty("stop_loss", NullValueHandling = NullValueHandling.Ignore)]
        public StopLoss StopLoss { get; set; }

        /// <summary>
        /// Contains information where the contract will be closed automatically when the value of
        /// the contract is close to zero. This is set by the us.
        /// </summary>
        [JsonProperty("stop_out", NullValueHandling = NullValueHandling.Ignore)]
        public StopOut StopOut { get; set; }

        /// <summary>
        /// Contain information where the contract will be closed automatically at the profit
        /// specified by the user.
        /// </summary>
        [JsonProperty("take_profit", NullValueHandling = NullValueHandling.Ignore)]
        public TakeProfit TakeProfit { get; set; }
    }

    /// <summary>
    /// Contains information where the contract will be closed automatically at the loss
    /// specified by the user.
    /// </summary>
    public partial class StopLoss
    {
        /// <summary>
        /// Localized display name
        /// </summary>
        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Stop loss amount
        /// </summary>
        [JsonProperty("order_amount")]
        public double OrderAmount { get; set; }

        /// <summary>
        /// Stop loss order epoch
        /// </summary>
        [JsonProperty("order_date", NullValueHandling = NullValueHandling.Ignore)]
        public long OrderDate { get; set; }

        /// <summary>
        /// Pip-sized barrier value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// Contains information where the contract will be closed automatically when the value of
    /// the contract is close to zero. This is set by the us.
    /// </summary>
    public partial class StopOut
    {
        /// <summary>
        /// Localized display name
        /// </summary>
        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Stop out amount
        /// </summary>
        [JsonProperty("order_amount", NullValueHandling = NullValueHandling.Ignore)]
        public double OrderAmount { get; set; }

        /// <summary>
        /// Stop out order epoch
        /// </summary>
        [JsonProperty("order_date", NullValueHandling = NullValueHandling.Ignore)]
        public long OrderDate { get; set; }

        /// <summary>
        /// Pip-sized barrier value
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    /// <summary>
    /// Contain information where the contract will be closed automatically at the profit
    /// specified by the user.
    /// </summary>
    public partial class TakeProfit
    {
        /// <summary>
        /// Localized display name
        /// </summary>
        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Take profit amount
        /// </summary>
        [JsonProperty("order_amount")]
        public double OrderAmount { get; set; }

        /// <summary>
        /// Take profit order epoch
        /// </summary>
        [JsonProperty("order_date", NullValueHandling = NullValueHandling.Ignore)]
        public long OrderDate { get; set; }

        /// <summary>
        /// Pip-sized barrier value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class TickStream
    {
        /// <summary>
        /// Epoch time of a tick or the contract start or end time.
        /// </summary>
        [JsonProperty("epoch", NullValueHandling = NullValueHandling.Ignore)]
        public long Epoch { get; set; }

        /// <summary>
        /// The spot value at the given epoch.
        /// </summary>
        [JsonProperty("tick")]
        public double Tick { get; set; }

        /// <summary>
        /// The spot value with the correct precision at the given epoch.
        /// </summary>
        [JsonProperty("tick_display_value")]
        public string TickDisplayValue { get; set; }
    }

    /// <summary>
    /// Every contract has buy and sell transaction ids, i.e. when you purchase a contract we
    /// associate it with buy transaction id, and if contract is already sold we associate that
    /// with sell transaction id.
    /// </summary>
    public partial class TransactionIdsForContract
    {
        /// <summary>
        /// Buy transaction ID for that contract
        /// </summary>
        [JsonProperty("buy", NullValueHandling = NullValueHandling.Ignore)]
        public long Buy { get; set; }

        /// <summary>
        /// Sell transaction ID for that contract, only present when contract is already sold.
        /// </summary>
        [JsonProperty("sell", NullValueHandling = NullValueHandling.Ignore)]
        public long Sell { get; set; }
    }

    /// <summary>
    /// Action name of the request made.
    /// </summary>
    public enum MsgType { ProposalOpenContract };

    public enum ContractStatus { Cancelled, Lost, Open, Sold, Won };

    public static class ProposalOpenConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                MsgTypeProprosalOpenConverter.Singleton,
                ContractStatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public class MsgTypeProprosalOpenConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MsgType) || t == typeof(MsgType);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "proposal_open_contract")
            {
                return MsgType.ProposalOpenContract;
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

            if (value == MsgType.ProposalOpenContract)
            {
                serializer.Serialize(writer, "proposal_open_contract");
                return;
            }

            throw new Exception("Cannot marshal type MsgType");
        }

         public static readonly MsgTypeProprosalOpenConverter Singleton = new MsgTypeProprosalOpenConverter();
    }

    internal class ContractStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ContractStatus) || t == typeof(ContractStatus);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "cancelled":
                    return ContractStatus.Cancelled;
                case "lost":
                    return ContractStatus.Lost;
                case "open":
                    return ContractStatus.Open;
                case "sold":
                    return ContractStatus.Sold;
                case "won":
                    return ContractStatus.Won;
            }
            throw new Exception("Cannot unmarshal type ContractStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ContractStatus)untypedValue;
            switch (value)
            {
                case ContractStatus.Cancelled:
                    serializer.Serialize(writer, "cancelled");
                    return;
                case ContractStatus.Lost:
                    serializer.Serialize(writer, "lost");
                    return;
                case ContractStatus.Open:
                    serializer.Serialize(writer, "open");
                    return;
                case ContractStatus.Sold:
                    serializer.Serialize(writer, "sold");
                    return;
                case ContractStatus.Won:
                    serializer.Serialize(writer, "won");
                    return;
            }
            throw new Exception("Cannot marshal type ContractStatus");
        }

        public static readonly ContractStatusConverter Singleton = new ContractStatusConverter();
    }
}
