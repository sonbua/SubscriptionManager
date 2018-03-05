using System.Configuration;
using R2.Net.Mail.SystemNetSmtp;

namespace SubscriptionManager.Services.DependencyRegistration.Autofac
{
    public class SystemNetSmtpMailServiceSettings : ISystemNetSmtpMailServiceSettings
    {
        public string Server => ConfigurationManager.AppSettings["mail:smtp:server"];

        public int Port => int.Parse(ConfigurationManager.AppSettings["mail:smtp:port"]);

        public bool UseSsl => bool.Parse(ConfigurationManager.AppSettings["mail:smtp:useSsl"]);
    }
}