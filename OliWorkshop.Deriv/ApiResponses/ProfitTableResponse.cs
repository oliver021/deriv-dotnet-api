namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// A summary of account profit table is received
    /// </summary>
    public partial class ProfitTableResponse
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
        public string MsgType { get; set; }

        /// <summary>
        /// Account Profit Table.
        /// </summary>
        [JsonProperty("profit_table", NullValueHandling = NullValueHandling.Ignore)]
        public ProfitTable ProfitTable { get; set; }

        /// <summary>
        /// Optional field sent in request to map to response, present only when request contains
        /// `req_id`.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }
    }

    /// <summary>
    /// Account Profit Table.
    /// </summary>
    public partial class ProfitTable
    {
        /// <summary>
        /// Number of transactions returned in this call
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public double? Count { get; set; }

        /// <summary>
        /// Array of returned transactions
        /// </summary>
        [JsonProperty("transactions", NullValueHandling = NullValueHandling.Ignore)]
        public Transaction[] Transactions { get; set; }
    }

    public partial class Transaction
    {
        /// <summary>
        /// ID of the application where this contract was purchased.
        /// </summary>
        [JsonProperty("app_id")]
        public long? AppId { get; set; }

        /// <summary>
        /// The buy price
        /// </summary>
        [JsonProperty("buy_price", NullValueHandling = NullValueHandling.Ignore)]
        public double? BuyPrice { get; set; }

        /// <summary>
        /// The unique contract identifier.
        /// </summary>
        [JsonProperty("contract_id")]
        public long? ContractId { get; set; }

        /// <summary>
        /// The description of contract purchased if description is set to 1
        /// </summary>
        [JsonProperty("longcode", NullValueHandling = NullValueHandling.Ignore)]
        public string Longcode { get; set; }

        /// <summary>
        /// Payout price
        /// </summary>
        [JsonProperty("payout", NullValueHandling = NullValueHandling.Ignore)]
        public double? Payout { get; set; }

        /// <summary>
        /// Epoch purchase time of the transaction
        /// </summary>
        [JsonProperty("purchase_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? PurchaseTime { get; set; }

        /// <summary>
        /// The price the contract sold for.
        /// </summary>
        [JsonProperty("sell_price", NullValueHandling = NullValueHandling.Ignore)]
        public double? SellPrice { get; set; }

        /// <summary>
        /// Epoch sell time of the transaction
        /// </summary>
        [JsonProperty("sell_time")]
        public long? SellTime { get; set; }

        /// <summary>
        /// Compact description of the contract purchased if description is set to 1
        /// </summary>
        [JsonProperty("shortcode", NullValueHandling = NullValueHandling.Ignore)]
        public string Shortcode { get; set; }

        /// <summary>
        /// The transaction Identifier. Every contract (buy or sell) and every payment has a unique
        /// transaction identifier.
        /// </summary>
        [JsonProperty("transaction_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? TransactionId { get; set; }
    }
}
