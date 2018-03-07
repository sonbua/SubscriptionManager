using System.Threading.Tasks;
using R2;
using Raven.Client;

namespace SubscriptionManager.Subscriptions.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : CommandHandler<DeleteSubscriptionCommand>
    {
        private readonly IAsyncDocumentSession _session;

        public DeleteSubscriptionCommandHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        protected override async Task HandleCommandAsync(DeleteSubscriptionCommand command)
        {
            _session.Delete(command.Subscription);

            await _session.SaveChangesAsync();
        }
    }
}