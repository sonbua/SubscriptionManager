using System;
using System.ComponentModel.DataAnnotations;

namespace SubscriptionManager.Subscriptions.AddSubscription
{
    public class AddSubscriptionCommand
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public DateTime? StartDate { get; set; }

        public int? DurationInMonths { get; set; }
    }
}