using OliWorkshop.Deriv.ApiResponse;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{
    /// <summary>
    /// The proposal handler is a object you can use to reatrive data of princes
    /// </summary>
    public class ProposalHandler
    {
        internal DerivApiService _api;

        internal Proposal _proposal;

        /// <summary>
        /// internal constructor
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="proposal"></param>
        internal ProposalHandler(DerivApiService api, Proposal proposal)
        {
            _api = api;
            _proposal = proposal;
        }

        public double Payout { get => _proposal.AskPrice; }

        public double Percent { get => _proposal.Commission; }

        public string Title { get => _proposal.Longcode; }

        public string DisplayPrice { get => _proposal.DisplayValue; }

        public Task<ContractHandler> Buy() => _api.BuyContract(_proposal.Id, Payout);
    }
}