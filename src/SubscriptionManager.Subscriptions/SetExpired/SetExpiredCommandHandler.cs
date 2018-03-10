using System;
using System.Threading.Tasks;
using R2;
using Raven.Client;

namespace SubscriptionManager.Subscriptions.SetExpired
{
    /// <summary>
    /// Handles <see cref="SetExpiredCommand"/>.
    /// </summary>
    public class SetExpiredCommandHandler : CommandHandler<SetExpiredCommand>
    {
        private readonly IDocumentStore _store;

        /// <inheritdoc />
        public SetExpiredCommandHandler(IDocumentStore store)
        {
            _store = store;
        }

        /// <inheritdoc />
        protected override async Task HandleCommandAsync(SetExpiredCommand command)
        {
            using (var session = _store.OpenAsyncSession())
            {
                var today = DateTime.Now;

                var expiredEndDate =
                    new DateTime(
                        today.Year,
                        today.Month,
                        today.Day
                    );

                command.Subscription.EndDate = expiredEndDate;

                await session.StoreAsync(command.Subscription);
                await session.SaveChangesAsync();
            }
        }
    }
}