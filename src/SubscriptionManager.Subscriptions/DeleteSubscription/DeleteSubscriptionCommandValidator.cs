using System;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.SetExpired.Rule;

namespace SubscriptionManager.Subscriptions.DeleteSubscription
{
    /// <summary>
    /// Validates <see cref="DeleteSubscriptionCommand"/>.
    /// </summary>
    public class DeleteSubscriptionCommandValidator : RuleBasedValidator<DeleteSubscriptionCommand>
    {
        /// <inheritdoc />
        public DeleteSubscriptionCommandValidator(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddRule<SubscriptionMustExistRule>();
        }
    }
}