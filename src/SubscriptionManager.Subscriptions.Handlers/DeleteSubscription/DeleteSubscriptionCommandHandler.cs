using System.Threading.Tasks;
using R2;
using R2.Helper;
using Raven.Client;

namespace SubscriptionManager.Subscriptions.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : CommandHandler<DeleteSubscriptionCommand>
    {
        private readonly IRequestContext _requestContext;
        private readonly IAsyncDocumentSession _session;

        public DeleteSubscriptionCommandHandler(IRequestContext requestContext, IAsyncDocumentSession session)
        {
            _requestContext = requestContext;
            _session = session;
        }

        protected override async Task HandleCommandAsync(DeleteSubscriptionCommand command)
        {
            var subscription = _requestContext.TempData.CastTo<Subscription>();

            _session.Delete(subscription);

            await _session.SaveChangesAsync();
        }
    }
}