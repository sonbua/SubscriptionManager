using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
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

        /// <summary>
        /// Temporary object to be mapped in the handler. Should redesign this kind of temporal objects.
        /// </summary>
        [JsonIgnore]
        public Subscription Subscription { get; set; }
    }
}