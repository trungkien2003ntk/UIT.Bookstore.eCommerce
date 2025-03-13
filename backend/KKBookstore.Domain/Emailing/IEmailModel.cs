namespace KKBookstore.Domain.Emailing;

public interface IEmailModel
{
    string TemplateName { get; }
    object TemplateDataModel { get; }
    string Subject { get; }
    string? ReceiverFullName { get; set; }
}
