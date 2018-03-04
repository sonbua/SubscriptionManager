using Autofac;
using R2.Net.Mail.SystemNetSmtp;

namespace R2.Net.Mail.DependencyRegistration.Autofac
{
    public class R2NetMailModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SystemNetSmtpMailService>()
                .As<IMailService>()
                .InstancePerLifetimeScope();
        }
    }
}