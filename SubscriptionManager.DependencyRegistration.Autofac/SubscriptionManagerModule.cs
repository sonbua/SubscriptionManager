using Autofac;
using R2.Net.Mail.SystemNetSmtp;

namespace SubscriptionManager.DependencyRegistration.Autofac
{
    public class SubscriptionManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SystemNetSmtpMailServiceSettings>()
                .As<ISystemNetSmtpMailServiceSettings>()
                .InstancePerLifetimeScope();
        }
    }
}