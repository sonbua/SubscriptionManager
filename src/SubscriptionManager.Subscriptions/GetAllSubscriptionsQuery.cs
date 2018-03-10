using System;
using System.Collections.Generic;
using R2;

namespace SubscriptionManager.Subscriptions
{
    /// <summary>
    /// Get all subscriptions, including expired ones.
    /// </summary>
    public class GetAllSubscriptionsQuery : IQuery<IList<SubscriptionDto>>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class SubscriptionDto
    {
        /// <summary>
        /// Full name of those who subscribe to newsletter.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Email address of those who subscribes to newsletter.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Subscription's start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Subscription's end date.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}