using System;
using System.Runtime.Serialization;
using R2;

namespace SubscriptionManager.Subscriptions.SetExpired.Exception
{
    [Serializable]
    public class SubscriptionMustExistException : R2Exception
    {
        public SubscriptionMustExistException()
        {
        }

        public SubscriptionMustExistException(string message)
            : base(message)
        {
        }

        public SubscriptionMustExistException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected SubscriptionMustExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}