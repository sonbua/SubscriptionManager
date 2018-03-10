using System;
using System.ComponentModel.DataAnnotations;
using R2;

namespace SubscriptionManager.Subscriptions.AddSubscription
{
    /// <summary>
    /// Adds a new subscription.
    /// </summary>
    public class AddSubscriptionCommand : ICommand
    {
        /// <summary>
        /// Full name of those who subscribe to newsletter.
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Email address of those who subscribes to newsletter.
        /// </summary>
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Subscription's start date. Set to today by default.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Duration, in months, the subscription will take. Set to 1 by default.
        /// </summary>
        public int? DurationInMonths { get; set; }
    }
}