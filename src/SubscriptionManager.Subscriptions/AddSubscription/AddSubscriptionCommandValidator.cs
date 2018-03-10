using System;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.AddSubscription.Rule;

namespace SubscriptionManager.Subscriptions.AddSubscription
{
    /// <summary>
    /// Validate <see cref="AddSubscriptionCommand"/>.
    /// </summary>
    public class AddSubscriptionCommandValidator : RuleBasedValidator<AddSubscriptionCommand>
    {
        /// <inheritdoc />
        public AddSubscriptionCommandValidator(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddRule<DurationInMonthsMustBeGreaterThanOrEqualTo1Rule>();
            AddRule<EndDateMustBeInTheFutureRule>();
        }
    }
}