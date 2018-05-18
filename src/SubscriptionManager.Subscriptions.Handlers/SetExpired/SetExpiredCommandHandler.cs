using System;
using System.Threading.Tasks;
using R2;
using R2.Helper;

namespace SubscriptionManager.Subscriptions.SetExpired
{
    public class SetExpiredCommandHandler : CommandHandler<SetExpiredCommand>
    {
        private readonly IRequestContext _requestContext;
        private readonly ISubscriptionRepository _repository;

        public SetExpiredCommandHandler(IRequestContext requestContext, ISubscriptionRepository repository)
        {
            _requestContext = requestContext;
            _repository = repository;
        }

        protected override async Task HandleCommandAsync(SetExpiredCommand command)
        {
            var today = DateTime.Now;

            var expiredEndDate =
                new DateTime(
                    today.Year,
                    today.Month,
                    today.Day
                );

            var subscription = _requestContext.TempData.CastTo<Subscription>();

            subscription.EndDate = expiredEndDate;

            await _repository.UpdateSubscriptionAsync(subscription);
        }
    }
}