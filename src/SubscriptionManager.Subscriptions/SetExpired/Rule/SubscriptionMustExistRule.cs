using System.Threading.Tasks;
using R2.Aspect.Validation;
using Raven.Client;
using SubscriptionManager.Subscriptions.SetExpired.Exception;

namespace SubscriptionManager.Subscriptions.SetExpired.Rule
{
    public class SubscriptionMustExistRule : IValidationRule<SetExpiredCommand>
    {
        private readonly IDocumentStore _store;

        public SubscriptionMustExistRule(IDocumentStore store)
        {
            _store = store;
        }

        public async Task TestAsync(SetExpiredCommand command)
        {
            using (var session = _store.OpenAsyncSession())
            {
                var subscription = await session.LoadAsync<Subscription>(command.SubscriptionId);

                if (subscription == null)
                {
                    throw new SubscriptionMustExistException();
                }

                command.Subscription = subscription;
            }
        }
    }
}