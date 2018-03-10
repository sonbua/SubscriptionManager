using System;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.AddSubscription.Rule;

namespace SubscriptionManager.Subscriptions.AddSubscription
{
    public class AddSubscriptionCommandValidator : RuleBasedValidator<AddSubscriptionCommand>
    {
        public AddSubscriptionCommandValidator(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddRule<EmailAddressMustBeUniqueRule>();
            AddRule<DurationInMonthsMustBeGreaterThanOrEqualTo1Rule>();
            AddRule<EndDateMustBeInTheFutureRule>();
        }
    }
}