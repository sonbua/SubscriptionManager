using System.Threading.Tasks;
using R2;
using Raven.Client;

namespace SubscriptionManager.Subscriptions.SetExpired
{
    public class SetExpiredCommandHandler : CommandHandler<SetExpiredCommand>
    {
        private readonly IDocumentStore _store;

        public SetExpiredCommandHandler(IDocumentStore store)
        {
            _store = store;
        }

        protected override async Task HandleCommandAsync(SetExpiredCommand command)
        {
            using (var session = _store.OpenAsyncSession())
            {
                command.Subscription.IsDeleted = true;

                await session.StoreAsync(command.Subscription);
            }
        }
    }
}