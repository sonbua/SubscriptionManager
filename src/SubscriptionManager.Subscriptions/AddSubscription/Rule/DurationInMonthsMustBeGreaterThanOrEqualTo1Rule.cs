using System.Threading.Tasks;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.AddSubscription.Exception;

namespace SubscriptionManager.Subscriptions.AddSubscription.Rule
{
    public class DurationInMonthsMustBeGreaterThanOrEqualTo1Rule : IValidationRule<AddSubscriptionCommand>
    {
        public Task TestAsync(AddSubscriptionCommand command)
        {
            if (command.DurationInMonths.Value < 1)
            {
                throw new DurationInMonthsMustBeGreaterThanOrEqualTo1Exception();
            }

            return Task.FromResult(0);
        }
    }
}