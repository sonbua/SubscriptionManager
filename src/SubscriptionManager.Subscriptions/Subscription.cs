using System;

namespace SubscriptionManager.Subscriptions
{
    public class Subscription
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}