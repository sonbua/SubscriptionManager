using System;
using System.Threading.Tasks;
using R2;
using R2.Aspect.Validation;
using R2.Helper;
using SubscriptionManager.Subscriptions.SetExpired.Exception;

namespace SubscriptionManager.Subscriptions.SetExpired.Rule
{
    public class SubscriptionMustBeNotYetExpiredRule : IValidationRule<SetExpiredCommand>
    {
        private readonly IRequestContext _requestContext;

        public SubscriptionMustBeNotYetExpiredRule(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        public Task TestAsync(SetExpiredCommand command)
        {
            var subscription = _requestContext.TempData.CastTo<Subscription>();
            
            if (subscription.EndDate <= DateTime.Now)
            {
                throw new SubscriptionMustBeNotYetExpiredException();
            }
            
            return Task.FromResult(0);
        }
    }
}