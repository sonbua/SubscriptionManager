using System.Threading.Tasks;
using R2;

namespace SubscriptionManager.Subscriptions.GetSubscriptionById
{
    public class GetSubscriptionByIdQueryHandler : QueryHandler<GetSubscriptionByIdQuery, SubscriptionDto>
    {
        protected override Task<SubscriptionDto> HandleQueryAsync(GetSubscriptionByIdQuery query)
        {
            var subscription = query.Subscription;

            var subscriptionDto = new SubscriptionDto
            {
                Id = subscription.Id,
                FullName = subscription.FullName,
                EmailAddress = subscription.EmailAddress,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate
            };

            return Task.FromResult(subscriptionDto);
        }
    }
}