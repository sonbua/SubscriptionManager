using System.ComponentModel.DataAnnotations;
using R2;

namespace SubscriptionManager.Subscriptions.SetExpired
{
    public class SetExpiredCommand : ICommand
    {
        [Required]
        public string SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }
    }
}