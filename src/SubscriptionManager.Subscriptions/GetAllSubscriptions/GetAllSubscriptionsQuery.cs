using System;
using System.Collections.Generic;
using R2;

namespace SubscriptionManager.Subscriptions.GetAllSubscriptions
{
    public class GetAllSubscriptionsQuery : IQuery<IList<SubscriptionDto>>
    {
    }

    public class SubscriptionDto
    {
        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}