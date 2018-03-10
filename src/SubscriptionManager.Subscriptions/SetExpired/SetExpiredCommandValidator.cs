using System;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.SetExpired.Rule;

namespace SubscriptionManager.Subscriptions.SetExpired
{
    /// <summary>
    /// Validates <see cref="SetExpiredCommand"/>.
    /// </summary>
    public class SetExpiredCommandValidator : RuleBasedValidator<SetExpiredCommand>
    {
        /// <inheritdoc />
        public SetExpiredCommandValidator(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            AddRule<SubscriptionMustExistRule>();
        }
    }
}