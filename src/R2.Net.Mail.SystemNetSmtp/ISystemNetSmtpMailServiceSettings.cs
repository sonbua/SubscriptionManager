namespace R2.Net.Mail.SystemNetSmtp
{
    public interface ISystemNetSmtpMailServiceSettings
    {
        string Server { get; }

        int Port { get; }

        bool UseSsl { get; }
    }
}