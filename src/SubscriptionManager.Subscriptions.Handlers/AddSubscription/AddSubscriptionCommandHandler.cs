using System.Threading.Tasks;
using R2;

namespace SubscriptionManager.Subscriptions.AddSubscription
{
    public class AddSubscriptionCommandHandler : CommandHandler<AddSubscriptionCommand>
    {
        private readonly ISubscriptionRepository _repository;

        public AddSubscriptionCommandHandler(ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        protected override async Task HandleCommandAsync(AddSubscriptionCommand command)
        {
            var subscription = new Subscription
            {
                FullName = command.FullName,
                EmailAddress = command.EmailAddress,
                StartDate = command.StartDate.Value,
                EndDate = command.StartDate.Value.AddMonths(command.DurationInMonths.Value),
            };

            await _repository.AddSubscriptionAsync(subscription);
        }
    }
}