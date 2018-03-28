using System;
using System.Runtime.Serialization;
using R2;

namespace SubscriptionManager.Subscriptions.SetExpired.Exception
{
    [Serializable]
    public class SubscriptionMustBeNotYetExpiredException : R2Exception
    {
        public SubscriptionMustBeNotYetExpiredException()
        {
        }

        public SubscriptionMustBeNotYetExpiredException(string message)
            : base(message)
        {
        }

        public SubscriptionMustBeNotYetExpiredException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected SubscriptionMustBeNotYetExpiredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}