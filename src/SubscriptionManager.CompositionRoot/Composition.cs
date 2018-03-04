using System;
using Autofac;
using R2.DependencyRegistration.Autofac;
using R2.Net.Mail.DependencyRegistration.Autofac;
using SubscriptionManager.DependencyRegistration.Autofac;

namespace SubscriptionManager.CompositionRoot
{
    public static class Composition
    {
        public static void Load(Action<IServiceProvider> actionOnRegistrationCompleted)
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterModule<R2Module>()
                .RegisterModule<R2NetMailModule>()
                .RegisterModule<SubscriptionManagerModule>();

            var container = builder.Build();

            actionOnRegistrationCompleted(new AutofacServiceProvider(container));
        }
    }
}