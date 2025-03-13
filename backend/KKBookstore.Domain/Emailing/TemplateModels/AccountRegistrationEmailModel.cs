using KKBookstore.Domain.Shared.Emailing;

namespace KKBookstore.Domain.Emailing.TemplateModels;

public class AccountRegistrationEmailModel : IEmailModel
{
    public string RegistrationUrl { get; private set; }

    public string TemplateName => EmailConsts.AccountRegistrationEmailTemplateName;

    public object TemplateDataModel => new
    {
        ReceiverFullName,
        RegistrationUrl
    };


    public string Subject { get; }
    public string? ReceiverFullName { get; set; }

    public AccountRegistrationEmailModel(string registrationUrl)
    {
        Subject = EmailConsts.AccountRegistrationEmailSubject;
        RegistrationUrl = registrationUrl;
    }
}
