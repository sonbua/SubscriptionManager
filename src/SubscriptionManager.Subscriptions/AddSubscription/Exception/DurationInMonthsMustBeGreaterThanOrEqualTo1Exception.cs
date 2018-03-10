using System.Runtime.Serialization;
using R2;

namespace SubscriptionManager.Subscriptions.AddSubscription.Exception
{
    /// <inheritdoc />
    public class DurationInMonthsMustBeGreaterThanOrEqualTo1Exception : R2Exception
    {
        /// <inheritdoc />
        public DurationInMonthsMustBeGreaterThanOrEqualTo1Exception()
        {
        }

        /// <inheritdoc />
        public DurationInMonthsMustBeGreaterThanOrEqualTo1Exception(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        public DurationInMonthsMustBeGreaterThanOrEqualTo1Exception(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        protected DurationInMonthsMustBeGreaterThanOrEqualTo1Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}