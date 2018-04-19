using System.Threading.Tasks;
using R2;
using R2.Helper;

namespace SubscriptionManager.Subscriptions.GetSubscriptionById
{
    public class GetSubscriptionByIdQueryHandler : QueryHandler<GetSubscriptionByIdQuery, SubscriptionDto>
    {
        private readonly IRequestContext _requestContext;

        public GetSubscriptionByIdQueryHandler(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        protected override Task<SubscriptionDto> HandleQueryAsync(GetSubscriptionByIdQuery query)
        {
            var subscription = _requestContext.TempData.CastTo<Subscription>();

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