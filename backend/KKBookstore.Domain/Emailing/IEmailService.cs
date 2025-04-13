using KKBookstore.Domain.Emailing;
using KKBookstore.Domain.Emailing.Models;

namespace KKBookstore.Emailing;

public interface IEmailService
{
    Task SendAsync<T>(string to, string subject, T emailModel, List<string>? cc = null, List<EmailAttachment>? attachments = null)
        where T : IEmailModel;
}
