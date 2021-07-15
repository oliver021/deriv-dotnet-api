using OliWorkshop.Deriv.ApiRequest;
using OliWorkshop.Deriv.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{
    /// <summary>
    /// The contract handler is class to manage a lifetime comercial a purchase
    /// </summary>
    public class ContractHandler
    {
        public bool CanSell { get; }

        /// <summary>
        /// The main aspects of a contract
        /// </summary>
        public Buy BuyData { get; }

        /// <summary>
        /// The representative title of a contract
        /// </summary>
        public string Title { get => BuyData.Longcode; }

        /// <summary>
        /// Price at moment to purchase
        /// </summary>
        public double Buyed { get => BuyData.BuyPrice; }

        /// <summary>
        /// The indicative price of a purchase
        /// </summary>
        public double Payout { get => BuyData.Payout; }

        /// <summary>
        /// Epoch to finished a contract
        /// </summary>
        public DateTime Expire { get => DateTimeOffset.FromUnixTimeSeconds(1625955039).UtcDateTime; }

        public Error LastError { get; protected set; }

        internal WebSocketStream _ws;

        public ContractHandler(Buy buy, WebSocketStream ws)
        {
            _ws = ws;
            BuyData = buy;
        }

        /// <summary>
        /// Sell a contract buyed
        /// </summary>
        /// <param name="minimumPrice"></param>
        /// <returns></returns>
        public async Task<SellResult> SellAsync(double minimumPrice = 0)
        {
            // make a query
            var result = await  _ws.QueryAsync<SellRequest, SellResponse>(new SellRequest {
                Sell = BuyData.ContractId,
                Price = minimumPrice
            });

            if (result.Sell is null)
            {
                return new SellResult();
            }
            else
            {
                // set the results
                return new SellResult {
                    Contract = BuyData.ContractId,
                    Prince = result.Sell.SoldFor,
                    IsSell = true,
                    Transaction = result.Sell.TransactionId
                };
            }
        }

        /// <summary>
        /// Reatrive the current contract value
        /// </summary>
        /// <returns></returns>
        public async Task<double> GetCurrentPrice()
        {
            var data = await GetProposalAsync();
            return data.BidPrice;
        }

        /// <summary>
        /// Get proposal of a contract
        /// </summary>
        /// <returns></returns>
        public async Task<ProposalOpenContract> GetProposalAsync()
        {
            var result =  await _ws.QueryAsync<ProposalOpenRequest, ProposalOpenResponse>(new ProposalOpenRequest { 
                ContractId = BuyData.ContractId
            });

            return result.ProposalOpenContract;
        }
    }

    /// <summary>
    /// Simple class with struct to container result
    /// </summary>
    public class SellResult
    {
        public bool IsSell = false;
        public double Prince = 0;
        public long Contract = 0;
        public long Transaction;
    }
}
