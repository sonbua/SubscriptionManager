using System;

namespace SubscriptionManager.Desktop
{
    internal static class Container
    {
        public static IServiceProvider Instance { get; private set; }

        public static void SetContainer(IServiceProvider serviceProvider)
        {
            Instance = serviceProvider;
        }
    }
}