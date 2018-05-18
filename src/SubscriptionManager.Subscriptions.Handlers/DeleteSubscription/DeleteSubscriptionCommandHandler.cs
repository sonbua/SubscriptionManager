using System.Threading.Tasks;
using R2;
using R2.Helper;

namespace SubscriptionManager.Subscriptions.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : CommandHandler<DeleteSubscriptionCommand>
    {
        private readonly IRequestContext _requestContext;
        private readonly ISubscriptionRepository _repository;

        public DeleteSubscriptionCommandHandler(IRequestContext requestContext, ISubscriptionRepository repository)
        {
            _requestContext = requestContext;
            _repository = repository;
        }

        protected override async Task HandleCommandAsync(DeleteSubscriptionCommand command)
        {
            var subscription = _requestContext.TempData.CastTo<Subscription>();

            await _repository.DeleteSubscriptionAsync(subscription);
        }
    }
}