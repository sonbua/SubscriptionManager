using System.ComponentModel.DataAnnotations;
using R2;

namespace SubscriptionManager.Subscriptions
{
    /// <summary>
    /// Sets a subscription to expired status.
    /// </summary>
    public class SetExpiredCommand : ICommand
    {
        /// <summary>
        /// The ID of the subscription to be deleted.
        /// </summary>
        [Required]
        public string SubscriptionId { get; set; }
    }
}