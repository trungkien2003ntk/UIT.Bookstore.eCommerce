using KKBookstore.Domain.Shared.Emailing;

namespace KKBookstore.Domain.Emailing.TemplateModels;

public class ForgotPasswordEmailModel : IEmailModel
{
    public string TemplateName => EmailConsts.ForgotPasswordEmailTemplateName;

    public string Subject => EmailConsts.ForgotPasswordEmailSubject;

    public string? ReceiverFullName { get; set; }

    public string ResetPasswordUrl { get; }

    public object TemplateDataModel => new
    {
        ReceiverFullName,
        ResetPasswordUrl
    };

    public ForgotPasswordEmailModel(string receiverFullName, string resetPasswordUrl)
    {
        ReceiverFullName = receiverFullName;
        ResetPasswordUrl = resetPasswordUrl;
    }
}

