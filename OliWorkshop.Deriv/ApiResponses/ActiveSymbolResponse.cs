namespace OliWorkshop.Deriv.ApiResponse
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// A message containing the list of active symbols.
    /// </summary>
    public partial class ActiveSymbolResponse
    {
        /// <summary>
        /// List of active symbols.
        /// </summary>
        [JsonProperty("active_symbols", NullValueHandling = NullValueHandling.Ignore)]
        public ActiveSymbol[] ActiveSymbols { get; set; }

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
    }

    /// <summary>
    /// The information about each symbol.
    /// </summary>
    public partial class ActiveSymbol
    {
        /// <summary>
        /// `1` if the symbol is tradable in a forward starting contract, `0` if not.
        /// </summary>
        [JsonProperty("allow_forward_starting", NullValueHandling = NullValueHandling.Ignore)]
        public long? AllowForwardStarting { get; set; }

        /// <summary>
        /// Amount the data feed is delayed (in minutes) due to Exchange licensing requirements. Only
        /// returned on `full` active symbols call.
        /// </summary>
        [JsonProperty("delay_amount", NullValueHandling = NullValueHandling.Ignore)]
        public long? DelayAmount { get; set; }

        /// <summary>
        /// Display name.
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// `1` if market is currently open, `0` if closed.
        /// </summary>
        [JsonProperty("exchange_is_open")]
        public long ExchangeIsOpen { get; set; }

        /// <summary>
        /// Indicate if the market is open
        /// </summary>
        public bool Open { get => ExchangeIsOpen == 1; }

        /// <summary>
        /// `1` if market is currently open, `0` if closed.
        /// </summary>
        public bool Suspend { get => IsTradingSuspended == 1; }

        /// <summary>
        /// Exchange name (for underlyings listed on a Stock Exchange). Only returned on `full`
        /// active symbols call.
        /// </summary>
        [JsonProperty("exchange_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ExchangeName { get; set; }

        /// <summary>
        /// Intraday interval minutes. Only returned on `full` active symbols call.
        /// </summary>
        [JsonProperty("intraday_interval_minutes", NullValueHandling = NullValueHandling.Ignore)]
        public long? IntradayIntervalMinutes { get; set; }

        /// <summary>
        /// `1` indicates that trading is currently suspended, `0` if not.
        /// </summary>
        [JsonProperty("is_trading_suspended")]
        public long IsTradingSuspended { get; set; }

        /// <summary>
        /// Market category (forex, indices, etc).
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Translated market name.
        /// </summary>
        [JsonProperty("market_display_name")]
        public string MarketDisplayName { get; set; }

        /// <summary>
        /// Pip size (i.e. minimum fluctuation amount).
        /// </summary>
        [JsonProperty("pip")]
        public double Pip { get; set; }

        /// <summary>
        /// For stock indices, the underlying currency for that instrument. Only returned on `full`
        /// active symbols call.
        /// </summary>
        [JsonProperty("quoted_currency_symbol")]
        public string QuotedCurrencySymbol { get; set; } = string.Empty;

        /// <summary>
        /// Latest spot price of the underlying. Only returned on `full` active symbols call.
        /// </summary>
        [JsonProperty("spot")]
        public double Spot { get; set; } = 0;

        /// <summary>
        /// Number of seconds elapsed since the last spot price. Only returned on `full` active
        /// symbols call.
        /// </summary>
        [JsonProperty("spot_age", NullValueHandling = NullValueHandling.Ignore)]
        public string SpotAge { get; set; }

        /// <summary>
        /// Latest spot epoch time. Only returned on `full` active symbols call.
        /// </summary>
        [JsonProperty("spot_time", NullValueHandling = NullValueHandling.Ignore)]
        public string SpotTime { get; set; }

        /// <summary>
        /// Submarket name.
        /// </summary>
        [JsonProperty("submarket")]
        public string Submarket { get; set; }

        /// <summary>
        /// Translated submarket name.
        /// </summary>
        [JsonProperty("submarket_display_name")]
        public string SubmarketDisplayName { get; set; }

        /// <summary>
        /// The symbol code for this underlying.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Symbol type (forex, commodities, etc).
        /// </summary>
        [JsonProperty("symbol_type")]
        public string SymbolType { get; set; }
    }
}
