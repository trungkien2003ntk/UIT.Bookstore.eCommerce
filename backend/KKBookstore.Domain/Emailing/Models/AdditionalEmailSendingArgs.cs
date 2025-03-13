namespace KKBookstore.Domain.Emailing.Models;

[Serializable]
public class AdditionalEmailSendingArgs
{
    public List<string>? CC { get; set; }

    public List<EmailAttachment>? Attachments { get; set; }
}