using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using R2;

namespace SubscriptionManager.Subscriptions.DeleteSubscription
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

        /// <summary>
        /// Temporary object to be deleted in the handler. Should redesign this kind of temporal objects.
        /// </summary>
        [JsonIgnore]
        public Subscription Subscription { get; set; }
    }
}