using System;

namespace SubscriptionManager.Subscriptions
{
    /// <summary>
    /// 
    /// </summary>
    public class Subscription
    {
        /// <summary>
        /// ID of subscription.
        /// </summary>
        public string Id { get; set; }

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

        /// <summary>
        /// Indicate whether this subscription document is deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}