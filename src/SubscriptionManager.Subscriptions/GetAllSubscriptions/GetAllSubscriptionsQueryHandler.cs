using System.Collections.Generic;
using System.Threading.Tasks;
using R2;
using Raven.Client;
using Raven.Client.Linq;

namespace SubscriptionManager.Subscriptions.GetAllSubscriptions
{
    /// <summary>
    /// Handles <see cref="GetAllSubscriptionsQuery"/>.
    /// </summary>
    public class GetAllSubscriptionsQueryHandler : QueryHandler<GetAllSubscriptionsQuery, IList<SubscriptionDto>>
    {
        private readonly IAsyncDocumentSession _session;

        /// <inheritdoc />
        public GetAllSubscriptionsQueryHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        /// <inheritdoc />
        protected override async Task<IList<SubscriptionDto>> HandleQueryAsync(GetAllSubscriptionsQuery query)
        {
            return
                await _session.Query<Subscription>()
                    .Where(x => !x.IsDeleted)
                    .Select(
                        x => new SubscriptionDto
                        {
                            FullName = x.FullName,
                            EmailAddress = x.EmailAddress,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate
                        })
                    .OrderBy(x => x.FullName)
                    .ToListAsync();
        }
    }
}