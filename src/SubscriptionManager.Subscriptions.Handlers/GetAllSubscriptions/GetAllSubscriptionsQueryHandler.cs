using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using R2;

namespace SubscriptionManager.Subscriptions.GetAllSubscriptions
{
    public class GetAllSubscriptionsQueryHandler : QueryHandler<GetAllSubscriptionsQuery, IList<SubscriptionDto>>
    {
        private readonly ISubscriptionRepository _repository;

        public GetAllSubscriptionsQueryHandler(ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        protected override async Task<IList<SubscriptionDto>> HandleQueryAsync(GetAllSubscriptionsQuery query)
        {
            var subscriptions = await _repository.GetAllSubscriptionsAsync();

            return
                subscriptions
                    .Select(
                        x => new SubscriptionDto
                        {
                            Id = x.Id,
                            FullName = x.FullName,
                            EmailAddress = x.EmailAddress,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate
                        })
                    .OrderBy(x => x.FullName)
                    .ToList();
        }
    }
}