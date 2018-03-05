using Autofac;

namespace SubscriptionManager.Services
{
    internal static class Container
    {
        internal static ILifetimeScope Instance { get; private set; }

        public static void SetContainer(ILifetimeScope lifetimeScope)
        {
            Instance = lifetimeScope;
        }
    }
}