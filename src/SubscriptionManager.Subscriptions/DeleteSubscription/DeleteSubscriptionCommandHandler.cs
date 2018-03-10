using System.Threading.Tasks;
using R2;
using Raven.Client;

namespace SubscriptionManager.Subscriptions.DeleteSubscription
{
    /// <summary>
    /// Handles <see cref="DeleteSubscriptionCommand"/>
    /// </summary>
    public class DeleteSubscriptionCommandHandler : CommandHandler<DeleteSubscriptionCommand>
    {
        private readonly IAsyncDocumentSession _session;

        /// <inheritdoc />
        public DeleteSubscriptionCommandHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        protected override async Task HandleCommandAsync(DeleteSubscriptionCommand command)
        {
            _session.Delete(command.Subscription);

            await _session.SaveChangesAsync();
        }
    }
}