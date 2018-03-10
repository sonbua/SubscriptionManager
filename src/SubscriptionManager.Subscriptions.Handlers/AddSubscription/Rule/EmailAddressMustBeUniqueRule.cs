using System.Linq;
using System.Threading.Tasks;
using R2.Aspect.Validation;
using Raven.Client;
using Raven.Client.Linq;
using SubscriptionManager.Subscriptions.AddSubscription.Exception;

namespace SubscriptionManager.Subscriptions.AddSubscription.Rule
{
    public class EmailAddressMustBeUniqueRule : IValidationRule<AddSubscriptionCommand>
    {
        private readonly IDocumentSession _session;

        public EmailAddressMustBeUniqueRule(IDocumentSession session)
        {
            _session = session;
        }

        public Task TestAsync(AddSubscriptionCommand command)
        {
            var sameEmailExists =
                _session.Query<Subscription>()
                    .Where(x => !x.IsDeleted)
                    .Any(x => x.EmailAddress == command.EmailAddress);

            if (sameEmailExists)
            {
                throw new EmailAddressMustBeUniqueException();
            }

            return Task.FromResult(0);
        }
    }
}