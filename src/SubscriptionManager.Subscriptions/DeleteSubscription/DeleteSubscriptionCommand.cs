using System.ComponentModel.DataAnnotations;

namespace SubscriptionManager.Subscriptions.DeleteSubscription
{
    public class DeleteSubscriptionCommand
    {
        [Required]
        public string SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }
    }
}