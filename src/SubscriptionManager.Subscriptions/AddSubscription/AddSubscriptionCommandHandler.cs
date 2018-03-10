using System.Threading.Tasks;
using R2;
using Raven.Client;

namespace SubscriptionManager.Subscriptions.AddSubscription
{
    /// <summary>
    /// Handle <see cref="AddSubscriptionCommand"/>.
    /// </summary>
    public class AddSubscriptionCommandHandler : CommandHandler<AddSubscriptionCommand>
    {
        private readonly IDocumentStore _store;

        /// <inheritdoc />
        public AddSubscriptionCommandHandler(IDocumentStore store)
        {
            _store = store;
        }

        /// <inheritdoc />
        protected override async Task HandleCommandAsync(AddSubscriptionCommand command)
        {
            var subscription = new Subscription
            {
                FullName = command.FullName,
                EmailAddress = command.EmailAddress,
                StartDate = command.StartDate.Value,
                EndDate = command.StartDate.Value.AddMonths(command.DurationInMonths.Value),
            };

            using (var session = _store.OpenAsyncSession())
            {
                await session.StoreAsync(subscription);
                await session.SaveChangesAsync();
            }
        }
    }
}