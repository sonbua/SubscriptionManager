using Autofac;

namespace R2.Net.Mail.SystemNetSmtp.DependencyRegistration.Autofac
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