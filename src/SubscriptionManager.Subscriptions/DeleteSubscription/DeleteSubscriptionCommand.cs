using System.ComponentModel.DataAnnotations;
using R2;

namespace SubscriptionManager.Subscriptions.DeleteSubscription
{
    public class DeleteSubscriptionCommand : ICommand
    {
        [Required]
        public string SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }
    }
}