using System;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.SetExpired.Rule;

namespace SubscriptionManager.Subscriptions.GetSubscriptionById
{
    public class GetSubscriptionByIdQueryValidator : RuleBasedValidator<GetSubscriptionByIdQuery>
    {
        public GetSubscriptionByIdQueryValidator(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddRule<SubscriptionMustExistRule>();
        }
    }
}