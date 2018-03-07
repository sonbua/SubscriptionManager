using System;
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
                var currentEndDate = command.Subscription.EndDate;

                var expiredEndDate =
                    new DateTime(
                        currentEndDate.Year,
                        currentEndDate.Month,
                        currentEndDate.Day
                    );

                command.Subscription.EndDate = expiredEndDate;

                await session.StoreAsync(command.Subscription);
                await session.SaveChangesAsync();
            }
        }
    }
}