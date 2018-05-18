using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubscriptionManager.Subscriptions
{
    public interface ISubscriptionRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);

        Task DeleteSubscriptionAsync(Subscription subscription);

        Task<IList<Subscription>> GetAllSubscriptionsAsync();

        Task<IList<Subscription>> GetSubscriptionsByEmailAsync(string emailAddress);

        Task<Subscription> FindSubscriptionByIdAsync(string subscriptionId);

        Task UpdateSubscriptionAsync(Subscription subscription);
    }
}