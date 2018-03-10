using System;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.SetExpired.Rule;

namespace SubscriptionManager.Subscriptions.DeleteSubscription
{
    public class DeleteSubscriptionCommandValidator : RuleBasedValidator<DeleteSubscriptionCommand>
    {
        public DeleteSubscriptionCommandValidator(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddRule<SubscriptionMustExistRule>();
        }
    }
}