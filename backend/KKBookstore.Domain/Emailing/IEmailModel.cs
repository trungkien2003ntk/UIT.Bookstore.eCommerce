namespace KKBookstore.Domain.Emailing;

public interface IEmailModel
{
    string TemplateName { get; }
    string Subject { get; }
    string? ReceiverFullName { get; set; }
    object TemplateDataModel { get; }
}
