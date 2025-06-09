using KKBookstore.Emailing.Models;

namespace KKBookstore.Emailing;

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
        ));        // Forgot password email template
        AddTemplate(new TemplateDefinition(
            EmailConsts.ForgotPasswordEmailTemplateName,
            EmailConsts.ForgotPasswordEmailAsmNamespace,
            layout: EmailConsts.LayoutTemplateName
        ));

        // Admin auto-hidden rating email template
        AddTemplate(new TemplateDefinition(
            EmailConsts.AdminAutoHiddenRatingEmailTemplateName,
            EmailConsts.AdminAutoHiddenRatingEmailAsmNamespace,
            layout: EmailConsts.LayoutTemplateName
        ));

        // User hidden rating email template
        AddTemplate(new TemplateDefinition(
            EmailConsts.UserHiddenRatingEmailTemplateName,
            EmailConsts.UserHiddenRatingEmailAsmNamespace,
            layout: EmailConsts.LayoutTemplateName
        ));

        // User restored rating email template
        AddTemplate(new TemplateDefinition(
            EmailConsts.UserRestoredRatingEmailTemplateName,
            EmailConsts.UserRestoredRatingEmailAsmNamespace,
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
