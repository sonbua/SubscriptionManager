using System.Linq;
using System.Threading.Tasks;
using R2.Aspect.Validation;
using SubscriptionManager.Subscriptions.AddSubscription.Exception;

namespace SubscriptionManager.Subscriptions.AddSubscription.Rule
{
    public class EmailAddressMustBeUniqueRule : IValidationRule<AddSubscriptionCommand>
    {
        private readonly ISubscriptionRepository _repository;

        public EmailAddressMustBeUniqueRule(ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task TestAsync(AddSubscriptionCommand command)
        {
            var subscriptionsByEmail = await _repository.GetSubscriptionsByEmailAsync(command.EmailAddress);
            var sameEmailExists = subscriptionsByEmail.Any();

            if (sameEmailExists)
            {
                throw new EmailAddressMustBeUniqueException();
            }
        }
    }
}