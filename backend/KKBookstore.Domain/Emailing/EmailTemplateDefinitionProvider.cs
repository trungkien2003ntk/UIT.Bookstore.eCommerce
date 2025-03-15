using KKBookstore.Domain.Emailing.Models;
using KKBookstore.Domain.Shared.Emailing;

namespace KKBookstore.Domain.Emailing;

public class EmailTemplateDefinitionProvider
{
    private readonly Dictionary<string, TemplateDefinition> _templates = [];

    public EmailTemplateDefinitionProvider()
    {
        DefineTemplates();
    }

    private void DefineTemplates()
    {
        // Layout template
        AddTemplate(new TemplateDefinition(
            EmailConsts.LayoutTemplateName,
            EmailConsts.LayoutTemplateAsmNamespace,
            isLayout: true
        ));

        // Account registration email template
        AddTemplate(new TemplateDefinition(
            EmailConsts.AccountRegistrationEmailTemplateName,
            EmailConsts.AccountRegistrationEmailAsmNamespace,
            layout: EmailConsts.LayoutTemplateName
        ));

        // Forgot password email template
        AddTemplate(new TemplateDefinition(
            EmailConsts.ForgotPasswordEmailTemplateName,
            EmailConsts.ForgotPasswordEmailAsmNamespace,
            layout: EmailConsts.LayoutTemplateName
        ));

    }

    private void AddTemplate(TemplateDefinition templateDefinition)
    {
        _templates[templateDefinition.Name] = templateDefinition;
    }

    public TemplateDefinition GetTemplateDefinition(string name)
    {
        if (!_templates.TryGetValue(name, out var template))
        {
            throw new KeyNotFoundException($"Template '{name}' not found.");
        }

        return template;
    }
}
