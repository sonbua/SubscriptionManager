using System.ComponentModel.DataAnnotations;

namespace SubscriptionManager.Subscriptions.SetExpired
{
    public class SetExpiredCommand
    {
        [Required]
        public string SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }
    }
}