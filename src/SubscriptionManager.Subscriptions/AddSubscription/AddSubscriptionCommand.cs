using System;
using System.ComponentModel.DataAnnotations;
using R2;

namespace SubscriptionManager.Subscriptions.AddSubscription
{
    public class AddSubscriptionCommand : ICommand
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