using System.Threading.Tasks;
using R2.Aspect.Validation;
using Raven.Client;
using SubscriptionManager.Subscriptions.DeleteSubscription;
using SubscriptionManager.Subscriptions.SetExpired.Exception;

namespace SubscriptionManager.Subscriptions.SetExpired.Rule
{
    public class SubscriptionMustExistRule
        : IValidationRule<SetExpiredCommand>,
            IValidationRule<DeleteSubscriptionCommand>
    {
        private readonly IAsyncDocumentSession _session;

        public SubscriptionMustExistRule(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task TestAsync(SetExpiredCommand command)
        {
            command.Subscription = await GetSubscriptionAsync(command.SubscriptionId);
        }

        public async Task TestAsync(DeleteSubscriptionCommand command)
        {
            command.Subscription = await GetSubscriptionAsync(command.SubscriptionId);
        }

        private async Task<Subscription> GetSubscriptionAsync(string subscriptionId)
        {
            var subscription = await _session.LoadAsync<Subscription>(subscriptionId);

            if (subscription == null || subscription.IsDeleted)
            {
                throw new SubscriptionMustExistException();
            }

            return subscription;
        }
    }
}