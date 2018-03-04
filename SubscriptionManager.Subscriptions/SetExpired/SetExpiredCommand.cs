namespace SubscriptionManager.Subscriptions.SetExpired
{
    public class SetExpiredCommand
    {
        public string SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }
    }
}