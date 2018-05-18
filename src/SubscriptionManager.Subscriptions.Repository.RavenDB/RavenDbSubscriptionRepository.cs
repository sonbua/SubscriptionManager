using System.Collections.Generic;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Linq;

namespace SubscriptionManager.Subscriptions.Repository.RavenDB
{
    public class RavenDbSubscriptionRepository : ISubscriptionRepository
    {
        private readonly IAsyncDocumentSession _asyncSession;

        public RavenDbSubscriptionRepository(IAsyncDocumentSession asyncSession)
        {
            _asyncSession = asyncSession;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await _asyncSession.StoreAsync(subscription);
            await _asyncSession.SaveChangesAsync();
        }

        public async Task DeleteSubscriptionAsync(Subscription subscription)
        {
            _asyncSession.Delete(subscription);

            await _asyncSession.SaveChangesAsync();
        }

        public async Task<IList<Subscription>> GetAllSubscriptionsAsync()
        {
            return await _asyncSession.Query<Subscription>()
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<IList<Subscription>> GetSubscriptionsByEmailAsync(string emailAddress)
        {
            return await _asyncSession.Query<Subscription>()
                .Where(x => !x.IsDeleted)
                .Where(x => x.EmailAddress == emailAddress)
                .ToListAsync();
        }

        public async Task<Subscription> FindSubscriptionByIdAsync(string subscriptionId) =>
            await _asyncSession.LoadAsync<Subscription>(subscriptionId);

        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            await _asyncSession.StoreAsync(subscription);
            await _asyncSession.SaveChangesAsync();
        }
    }
}