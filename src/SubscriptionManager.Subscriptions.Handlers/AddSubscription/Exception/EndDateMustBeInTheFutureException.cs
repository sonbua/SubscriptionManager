using System;
using System.Runtime.Serialization;
using R2;

namespace SubscriptionManager.Subscriptions.AddSubscription.Exception
{
    [Serializable]
    public class EndDateMustBeInTheFutureException : R2Exception
    {
        public EndDateMustBeInTheFutureException()
        {
        }

        public EndDateMustBeInTheFutureException(string message)
            : base(message)
        {
        }

        public EndDateMustBeInTheFutureException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected EndDateMustBeInTheFutureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}