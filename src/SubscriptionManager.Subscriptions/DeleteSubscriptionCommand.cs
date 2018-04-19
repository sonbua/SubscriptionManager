using System.ComponentModel.DataAnnotations;
using R2;

namespace SubscriptionManager.Subscriptions
{
    /// <summary>
    /// Deletes a subscription.
    /// </summary>
    public class DeleteSubscriptionCommand : ICommand
    {
        /// <summary>
        /// The ID of the subscription to be deleted.
        /// </summary>
        [Required]
        public string SubscriptionId { get; set; }
    }
}