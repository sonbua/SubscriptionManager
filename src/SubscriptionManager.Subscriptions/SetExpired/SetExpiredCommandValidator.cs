using System;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.SetExpired.Rule;

namespace SubscriptionManager.Subscriptions.SetExpired
{
    public class SetExpiredCommandValidator : RuleBasedValidator<SetExpiredCommand>
    {
        public SetExpiredCommandValidator(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddRule<SubscriptionMustExistRule>();
        }
    }
}