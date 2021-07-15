namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using OliWorkshop.Deriv.ApiRequest;

    /// <summary>
    /// A summary of account statement is received
    /// </summary>
    public partial class StatementResponse
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
        /// Account statement.
        /// </summary>
        [JsonProperty("statement", NullValueHandling = NullValueHandling.Ignore)]
        public Statement Statement { get; set; }
    }

    /// <summary>
    /// Account statement.
    /// </summary>
    public partial class Statement
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
        public TransactionStatement[] Transactions { get; set; }
    }

    public partial class TransactionStatement
    {
        /// <summary>
        /// It is the type of action.
        /// </summary>
        [JsonProperty("action_type", NullValueHandling = NullValueHandling.Ignore)]
        public ActionType? ActionType { get; set; }

        /// <summary>
        /// It is the amount of transaction.
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double? Amount { get; set; }

        /// <summary>
        /// ID of the application where this contract was purchased.
        /// </summary>
        [JsonProperty("app_id")]
        public long? AppId { get; set; }

        /// <summary>
        /// It is the remaining balance.
        /// </summary>
        [JsonProperty("balance_after", NullValueHandling = NullValueHandling.Ignore)]
        public double? BalanceAfter { get; set; }

        /// <summary>
        /// It is the contract ID.
        /// </summary>
        [JsonProperty("contract_id")]
        public long? ContractId { get; set; }

        /// <summary>
        /// Contains details about fees used for transfer. It is present only when action type is
        /// transfer.
        /// </summary>
        [JsonProperty("fees", NullValueHandling = NullValueHandling.Ignore)]
        public Fees Fees { get; set; }

        /// <summary>
        /// Contains details of account from which amount was transferred. It is present only when
        /// action type is transfer.
        /// </summary>
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public From From { get; set; }

        /// <summary>
        /// The description of contract purchased if description is set to `1`.
        /// </summary>
        [JsonProperty("longcode", NullValueHandling = NullValueHandling.Ignore)]
        public string Longcode { get; set; }

        /// <summary>
        /// Payout price
        /// </summary>
        [JsonProperty("payout")]
        public double? Payout { get; set; }

        /// <summary>
        /// Time at which contract was purchased, present only for sell transaction
        /// </summary>
        [JsonProperty("purchase_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? PurchaseTime { get; set; }

        /// <summary>
        /// Internal transaction identifier for the corresponding buy transaction ( set only for
        /// contract selling )
        /// </summary>
        [JsonProperty("reference_id")]
        public long? ReferenceId { get; set; }

        /// <summary>
        /// Compact description of the contract purchased if description is set to `1`.
        /// </summary>
        [JsonProperty("shortcode")]
        public string Shortcode { get; set; }

        /// <summary>
        /// Contains details of account to which amount was transferred. It is present only when
        /// action type is transfer.
        /// </summary>
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public To To { get; set; }

        /// <summary>
        /// It is the transaction ID. In statement every contract (buy or sell) and every payment has
        /// a unique ID.
        /// </summary>
        [JsonProperty("transaction_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? TransactionId { get; set; }

        /// <summary>
        /// It is the time of transaction.
        /// </summary>
        [JsonProperty("transaction_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? TransactionTime { get; set; }

        /// <summary>
        /// Additional withdrawal details such as typical processing times, if description is set to
        /// `1`.
        /// </summary>
        [JsonProperty("withdrawal_details", NullValueHandling = NullValueHandling.Ignore)]
        public string WithdrawalDetails { get; set; }
    }

    /// <summary>
    /// Contains details about fees used for transfer. It is present only when action type is
    /// transfer.
    /// </summary>
    public partial class Fees
    {
        /// <summary>
        /// Fees amount
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double? Amount { get; set; }

        /// <summary>
        /// Fees currency
        /// </summary>
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        /// <summary>
        /// Minimum amount of fees
        /// </summary>
        [JsonProperty("minimum", NullValueHandling = NullValueHandling.Ignore)]
        public double? Minimum { get; set; }

        /// <summary>
        /// Fees percentage
        /// </summary>
        [JsonProperty("percentage", NullValueHandling = NullValueHandling.Ignore)]
        public double? Percentage { get; set; }
    }

    /// <summary>
    /// Contains details of account from which amount was transferred. It is present only when
    /// action type is transfer.
    /// </summary>
    public partial class From
    {
        /// <summary>
        /// Login id of the account from which money was transferred.
        /// </summary>
        [JsonProperty("loginid", NullValueHandling = NullValueHandling.Ignore)]
        public string Loginid { get; set; }
    }

    /// <summary>
    /// Contains details of account to which amount was transferred. It is present only when
    /// action type is transfer.
    /// </summary>
    public partial class To
    {
        /// <summary>
        /// Login id of the account to which money was transferred.
        /// </summary>
        [JsonProperty("loginid", NullValueHandling = NullValueHandling.Ignore)]
        public string Loginid { get; set; }
    }

    public static class StatementResponseConverter
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
}
