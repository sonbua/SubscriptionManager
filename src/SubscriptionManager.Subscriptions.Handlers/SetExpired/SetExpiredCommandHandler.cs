using System;
using System.Threading.Tasks;
using R2;
using R2.Helper;
using Raven.Client;

namespace SubscriptionManager.Subscriptions.SetExpired
{
    public class SetExpiredCommandHandler : CommandHandler<SetExpiredCommand>
    {
        private readonly IRequestContext _requestContext;
        private readonly IDocumentStore _store;

        public SetExpiredCommandHandler(IRequestContext requestContext, IDocumentStore store)
        {
            _requestContext = requestContext;
            _store = store;
        }

        protected override async Task HandleCommandAsync(SetExpiredCommand command)
        {
            using (var session = _store.OpenAsyncSession())
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

                await session.StoreAsync(subscription);
                await session.SaveChangesAsync();
            }
        }
    }
}