using System.Configuration;
using Autofac;
using R2.Net.Mail.SystemNetSmtp;
using Raven.Client.Document;

namespace SubscriptionManager.Services.DependencyRegistration.Autofac
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SystemNetSmtpMailServiceSettings>()
                .As<ISystemNetSmtpMailServiceSettings>()
                .InstancePerLifetimeScope();

            builder
                .RegisterInstance(
                    new DocumentStore
                    {
                        Url = ConfigurationManager.AppSettings["ravendb:serverUrl"],
                        DefaultDatabase = ConfigurationManager.AppSettings["ravendb:databaseName"],
                        Conventions = new DocumentConvention
                        {
                            IdentityPartsSeparator = "-"
                        }
                    }.Initialize())
                .SingleInstance();
        }
    }
}