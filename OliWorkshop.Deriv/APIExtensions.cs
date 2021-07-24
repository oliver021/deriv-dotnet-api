using OliWorkshop.Deriv.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{

    /// <summary>
    /// The enumration of the market category symbols
    /// </summary>
    public enum SymbolsMarkets
    {
        Forex,
        Indices,
        Synthetic_Index,
        Commodities,
        Crypto_Concurrency
    }

    /// <summary>
    /// Some helpers to short code
    /// </summary>
    public static class APIExtensions
    {
        /// <summary>
        /// const for forget requests
        /// </summary>
        public const string KeyTopUpVirtual = "topup_virtual";
        public const string KeyOfPing = "ping";
        public const string KeyOfLogout = "logout";
        public const string KeyOfTime = "time";
        public const string KeyOfSetConcurrency = "set_account_currency";
        public const string KeyOfForget = "forget";
        public const string KeyOfForgetAll = "forget_all";

        /// <summary>
        /// Shortcut for single poll ping request
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public static async Task<bool> PingAsync(this WebSocketStream ws)
        {
            var response = await ws.SinglePoll(new Dictionary<string, object>
            {
                [KeyOfPing] = 1
            });

            return response[KeyOfPing] == "pong";
        }

        /// <summary>
        /// Shortcut for single request to logout sessions
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public static async Task<bool> LogoutAsync(this WebSocketStream ws)
        {
            var response = await ws.SinglePoll(new Dictionary<string, object>
            {
                [KeyOfLogout] = 1
            });

            return response[KeyOfPing] == "1";
        }

        /// <summary>
        /// Shortcut for single request to logout sessions
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public static Task<bool> LogoutAsync(this DerivApiService deriv) => deriv._ws.LogoutAsync();
        
        /// <summary>
        /// Shortcut for deriv service
        /// </summary>
        /// <param name="deriv"></param>
        /// <param name="concurrency"></param>
        /// <returns></returns>
        public static Task<bool> LogoutAsync(this DerivApiService deriv, string concurrency)
        =>  deriv._ws.SetConcurrency(concurrency);

        /// <summary>
        /// Shortcut for deriv service
        /// </summary>
        /// <param name="deriv"></param>
        /// <returns></returns>
        public static Task<DateTimeOffset> PollTimeAsync(this DerivApiService deriv) => deriv._ws.Time();

        /// <summary>
        /// Shortcut for change concurrency in a request
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public static async Task<bool> SetConcurrency(this WebSocketStream ws, string concurrency)
        {
            var response = await ws.SinglePoll(new Dictionary<string, object>
            {
                [KeyOfSetConcurrency] = concurrency
            });

            return response[KeyOfSetConcurrency] == "1";
        }

        /// <summary>
        /// Reset balance of the virtual account
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="concurrency"></param>
        /// <returns></returns>
        public static async Task<bool> ResetVirtualBalance(this WebSocketStream ws, string concurrency)
        {
            var response = await ws.SinglePoll(new Dictionary<string, object>
            {
                [KeyTopUpVirtual] = 1
            });

            return response[KeyTopUpVirtual] == "1";
        }

        /// <summary>
        /// Shortcut for single poll time request
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public static async Task<DateTimeOffset> Time(this WebSocketStream ws)
        {
            var response = await ws.SinglePoll(new Dictionary<string, object> { 
                [KeyOfTime] = 1
            });

            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(response[KeyOfTime]));
        }

        /// <summary>
        /// Shortcut for single poll forget
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public static async Task<bool> Forget(this WebSocketStream ws, string subscription)
        {
            var response = await ws.SinglePoll(new Dictionary<string, object>
            {
                [KeyOfForget] = subscription
            });

            return response[KeyOfForget] == "1";
        }

        /// <summary>
        /// Shortcut for single poll forget all base on type of request
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public static Task ForgetAll(this WebSocketStream ws, string type)
        {
            return ws.SinglePoll(new Dictionary<string, object>
            {
                [KeyOfForgetAll] = type
            });

        }

        /// <summary>
        /// Reatrive an enumerable with filter criteria
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<ActiveSymbol> Filter(this IEnumerable<ActiveSymbol> symbols, string type)
        {
            if (symbols is null)
            {
                throw new ArgumentNullException(nameof(symbols));
            }

            return symbols.Where(x => x.Market == type);
        }

        /// <summary>
        /// Get only synthetic index category symbols
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        public static IEnumerable<ActiveSymbol> GetSyntheticalIndexes(this IEnumerable<ActiveSymbol> symbols)
        {
            if (symbols is null)
            {
                throw new ArgumentNullException(nameof(symbols));
            }

            return symbols.Filter(SymbolsMarkets.Synthetic_Index.ToString().ToLower());
        }

        /// <summary>
        /// Get only forex category symbols
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        public static IEnumerable<ActiveSymbol> GetForex(this IEnumerable<ActiveSymbol> symbols)
        {
            if (symbols is null)
            {
                throw new ArgumentNullException(nameof(symbols));
            }

            return symbols.Filter(SymbolsMarkets.Forex.ToString().ToLower());
        }

        /// <summary>
        /// Get only indices category symbols
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        public static IEnumerable<ActiveSymbol> GetIndices(this IEnumerable<ActiveSymbol> symbols)
        {
            if (symbols is null)
            {
                throw new ArgumentNullException(nameof(symbols));
            }

            return symbols.Filter(SymbolsMarkets.Indices.ToString().ToLower());
        }

        /// <summary>
        /// Get only indices category symbols
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        public static IEnumerable<ActiveSymbol> GetCommodities(this IEnumerable<ActiveSymbol> symbols)
        {
            if (symbols is null)
            {
                throw new ArgumentNullException(nameof(symbols));
            }

            return symbols.Filter(SymbolsMarkets.Commodities.ToString().ToLower());
        }
    }
}
