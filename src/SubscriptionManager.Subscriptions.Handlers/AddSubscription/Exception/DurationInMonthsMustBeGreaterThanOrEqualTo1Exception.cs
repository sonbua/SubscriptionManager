using System;
using System.Runtime.Serialization;
using R2;

namespace SubscriptionManager.Subscriptions.AddSubscription.Exception
{
    [Serializable]
    public class DurationInMonthsMustBeGreaterThanOrEqualTo1Exception : R2Exception
    {
        public DurationInMonthsMustBeGreaterThanOrEqualTo1Exception()
        {
        }

        public DurationInMonthsMustBeGreaterThanOrEqualTo1Exception(string message)
            : base(message)
        {
        }

        public DurationInMonthsMustBeGreaterThanOrEqualTo1Exception(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected DurationInMonthsMustBeGreaterThanOrEqualTo1Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}