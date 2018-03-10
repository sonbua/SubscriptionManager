using System.Runtime.Serialization;
using R2;

namespace SubscriptionManager.Subscriptions.AddSubscription.Exception
{
    /// <inheritdoc />
    public class EndDateMustBeInTheFutureException : R2Exception
    {
        /// <inheritdoc />
        public EndDateMustBeInTheFutureException()
        {
        }

        /// <inheritdoc />
        public EndDateMustBeInTheFutureException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        public EndDateMustBeInTheFutureException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        protected EndDateMustBeInTheFutureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}