using System;
using System.Collections.Generic;
using System.Text;

namespace OliWorkshop.Deriv.ApiResponse
{
    /// <summary>
    /// This interface represent an object that allow deliver through stream flow
    /// </summary>
    public interface IHasSubscription
    {
        /// <summary>
        /// Subscription contains the basic id of the current subscription
        /// </summary>
        public SubscriptionInformation Subscription { get; }
    }
}
