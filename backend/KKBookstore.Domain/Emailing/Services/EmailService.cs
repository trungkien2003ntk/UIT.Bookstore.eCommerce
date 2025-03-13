using KKBookstore.Domain.Emailing.Models;

namespace KKBookstore.Domain.Emailing.Services;

public class EmailService : IEmailService
{
    private readonly ITemplateRenderer _templateRenderer;
    private readonly IEmailSender _emailSender;

    public EmailService(ITemplateRenderer templateRenderer, IEmailSender emailSender)
    {
        _templateRenderer = templateRenderer;
        _emailSender = emailSender;
    }

    public async Task SendAsync<T>(string to, string subject, T emailModel, List<string>? cc = null, List<EmailAttachment>? attachments = null) where T : IEmailModel
    {
        var body = await _templateRenderer.RenderTemplateAsync(
            emailModel.TemplateName,
            emailModel.TemplateDataModel
        );

        await _emailSender.SendAsync(
            to,
            subject,
            body,
            isBodyHtml: true,
            additionalEmailSendingArgs: new()
            {
                CC = cc ?? [],
                Attachments = attachments ?? []
            });
    }
}
