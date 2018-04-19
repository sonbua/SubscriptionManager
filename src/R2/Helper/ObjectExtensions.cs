using System;

namespace R2.Helper
{
    public static class ObjectExtensions
    {
        public static T CastTo<T>(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (obj is T)
            {
                return (T) obj;
            }

            throw new ArgumentException($"{nameof(obj)} is not of type '{typeof(T)}'");
        }
    }
}