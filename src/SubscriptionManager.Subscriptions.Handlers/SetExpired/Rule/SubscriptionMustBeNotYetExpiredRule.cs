using System;
using System.Threading.Tasks;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.SetExpired.Exception;

namespace SubscriptionManager.Subscriptions.SetExpired.Rule
{
    public class SubscriptionMustBeNotYetExpiredRule : IValidationRule<SetExpiredCommand>
    {
        public Task TestAsync(SetExpiredCommand command)
        {
            if (command.Subscription.EndDate <= DateTime.Now)
            {
                throw new SubscriptionMustBeNotYetExpiredException();
            }
            
            return Task.FromResult(0);
        }
    }
}