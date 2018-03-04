using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace R2.Net.Mail.SystemNetSmtp
{
    public class SystemNetSmtpMailService : IMailService
    {
        private readonly ISystemNetSmtpMailServiceSettings _appSettings;

        public SystemNetSmtpMailService(ISystemNetSmtpMailServiceSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task SendAsync(MailMessage message)
        {
            using (var client = new SmtpClient(_appSettings.Server, _appSettings.Port)
            {
                EnableSsl = _appSettings.UseSsl
            })
            {
                using (var mailMessage = ConvertToMailMessage(message))
                {
                    await client.SendMailAsync(mailMessage);
                }
            }
        }

        private System.Net.Mail.MailMessage ConvertToMailMessage(MailMessage message)
        {
            var mailMessage = new System.Net.Mail.MailMessage();

            AddFrom(mailMessage, message.From);

            AddTo(mailMessage, message.To);

            AddSubject(mailMessage, message.Subject);

            AddBody(mailMessage, message.Body);

            AddAttachments(mailMessage, message.Attachment);

            return mailMessage;
        }

        private void AddFrom(System.Net.Mail.MailMessage mailMessage, MailAddress address)
        {
            mailMessage.From = new System.Net.Mail.MailAddress(address.Address, address.DisplayName);
        }

        private void AddTo(System.Net.Mail.MailMessage mailMessage, MailAddressCollection addresses)
        {
            foreach (var address in addresses)
            {
                mailMessage.To.Add(new System.Net.Mail.MailAddress(address.Address, address.DisplayName));
            }
        }

        private void AddSubject(System.Net.Mail.MailMessage mailMessage, string subject)
        {
            mailMessage.Subject = subject;
        }

        private void AddBody(System.Net.Mail.MailMessage mailMessage, string body)
        {
            mailMessage.Body = body;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
        }

        private void AddAttachments(System.Net.Mail.MailMessage mailMessage, MailAttachmentCollection attachments)
        {
            foreach (var attachment in attachments)
            {
                var contentBytes = Convert.FromBase64String(attachment.AttachmentContent);
                var contentStream = new MemoryStream(contentBytes);

                var convertedAttachment =
                    new Attachment(
                        contentStream,
                        attachment.AttachmentName,
                        attachment.AttachmentType)
                    {
                        TransferEncoding = TransferEncoding.Base64,
                    };

                mailMessage.Attachments.Add(convertedAttachment);
            }
        }
    }
}