using System.Threading.Tasks;

namespace R2.Net.Mail
{
    public interface IMailService
    {
        Task SendAsync(MailMessage message);
    }
}