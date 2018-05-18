using System.Threading.Tasks;
using R2;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.SetExpired.Exception;

namespace SubscriptionManager.Subscriptions.SetExpired.Rule
{
    public class SubscriptionMustExistRule
        : IValidationRule<SetExpiredCommand>,
            IValidationRule<DeleteSubscriptionCommand>,
            IValidationRule<GetSubscriptionByIdQuery>
    {
        private readonly IRequestContext _requestContext;
        private readonly ISubscriptionRepository _repository;

        public SubscriptionMustExistRule(IRequestContext requestContext, ISubscriptionRepository repository)
        {
            _requestContext = requestContext;
            _repository = repository;
        }

        public async Task TestAsync(SetExpiredCommand command)
        {
            _requestContext.TempData = await GetSubscriptionAsync(command.SubscriptionId);
        }

        public async Task TestAsync(DeleteSubscriptionCommand command)
        {
            _requestContext.TempData = await GetSubscriptionAsync(command.SubscriptionId);
        }

        public async Task TestAsync(GetSubscriptionByIdQuery query)
        {
            _requestContext.TempData = await GetSubscriptionAsync(query.SubscriptionId);
        }

        private async Task<Subscription> GetSubscriptionAsync(string subscriptionId)
        {
            var subscription = await _repository.FindSubscriptionByIdAsync(subscriptionId);

            if (subscription == null || subscription.IsDeleted)
            {
                throw new SubscriptionMustExistException();
            }

            return subscription;
        }
    }
}