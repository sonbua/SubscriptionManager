using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
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

        /// <summary>
        /// Temporary object to be deleted in the handler. Should redesign this kind of temporal objects.
        /// </summary>
        [JsonIgnore]
        public Subscription Subscription { get; set; }
    }
}