using System;
using Autofac;
using R2.DependencyRegistration.Autofac;
using R2.Net.Mail.SystemNetSmtp.DependencyRegistration.Autofac;
using SubscriptionManager.Services.DependencyRegistration.Autofac;
using SubscriptionManager.Subscriptions.DependencyRegistration.Autofac;

namespace SubscriptionManager.CompositionRoot
{
    public static class Composition
    {
        public static void Load(Action<ILifetimeScope> registrationCompleted)
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<AutofacServiceProvider>()
                .As<IServiceProvider>()
                .InstancePerLifetimeScope();

            builder
                .RegisterModule<R2Module>()
                .RegisterModule<R2NetMailModule>()
                .RegisterModule<SubscriptionsModule>()
                .RegisterModule<SubscriptionManagerModule>();

            var container = builder.Build();

            registrationCompleted(container);
        }
    }
}