using System;
using System.Threading.Tasks;
using R2.Aspect.Preprocessing;

namespace SubscriptionManager.Subscriptions.AddSubscription
{
    public class AddSubscriptionCommandPreprocessor : IPreprocessor<AddSubscriptionCommand>
    {
        public Task ProcessAsync(AddSubscriptionCommand command)
        {
            command.StartDate = command.StartDate ?? DateTime.Now;
            command.DurationInMonths = command.DurationInMonths ?? 1;

            return Task.FromResult(0);
        }
    }
}