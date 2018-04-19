using System.ComponentModel.DataAnnotations;
using R2;

namespace SubscriptionManager.Subscriptions
{
    /// <summary>
    /// Gets detail of an existing subscription.
    /// </summary>
    public class GetSubscriptionByIdQuery : IQuery<SubscriptionDto>
    {
        /// <summary>
        /// Subscription ID to load subscription's detail.
        /// </summary>
        [Required]
        public string SubscriptionId { get; set; }
    }
}