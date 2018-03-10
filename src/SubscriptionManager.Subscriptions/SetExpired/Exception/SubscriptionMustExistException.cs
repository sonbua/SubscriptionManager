using System.Runtime.Serialization;
using R2;

namespace SubscriptionManager.Subscriptions.SetExpired.Exception
{
    /// <inheritdoc />
    public class SubscriptionMustExistException : R2Exception
    {
        /// <inheritdoc />
        public SubscriptionMustExistException()
        {
        }

        /// <inheritdoc />
        public SubscriptionMustExistException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        public SubscriptionMustExistException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        protected SubscriptionMustExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}