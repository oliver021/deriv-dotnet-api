namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// A message with transaction results is received
    /// </summary>
    public partial class CancelContractResponse
    {
        /// <summary>
        /// Receipt for the transaction
        /// </summary>
        [JsonProperty("cancel", NullValueHandling = NullValueHandling.Ignore)]
        public Cancel Cancel { get; set; }

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
        /// Optional field sent in request to map to response, present only when request contains
        /// `req_id`.
        /// </summary>
        [JsonProperty("req_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReqId { get; set; }
    }

    /// <summary>
    /// Receipt for the transaction
    /// </summary>
    public partial class Cancel
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
        public long? ContractId { get; set; }

        /// <summary>
        /// Internal transaction identifier for the corresponding buy transaction
        /// </summary>
        [JsonProperty("reference_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReferenceId { get; set; }

        /// <summary>
        /// Actual effected sale price
        /// </summary>
        [JsonProperty("sold_for", NullValueHandling = NullValueHandling.Ignore)]
        public double? SoldFor { get; set; }

        /// <summary>
        /// Internal transaction identifier for the sale transaction
        /// </summary>
        [JsonProperty("transaction_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? TransactionId { get; set; }
    }
}

