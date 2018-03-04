using System;
using R2.Aspect.Validation;

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