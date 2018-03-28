using System;
using System.Runtime.Serialization;
using R2;

namespace SubscriptionManager.Subscriptions.AddSubscription.Exception
{
    [Serializable]
    public class EmailAddressMustBeUniqueException : R2Exception
    {
        public EmailAddressMustBeUniqueException()
        {
        }

        public EmailAddressMustBeUniqueException(string message)
            : base(message)
        {
        }

        public EmailAddressMustBeUniqueException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected EmailAddressMustBeUniqueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}