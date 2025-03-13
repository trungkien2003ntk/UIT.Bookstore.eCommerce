using KKBookstore.Domain.Emailing;
using KKBookstore.Domain.Emailing.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KKBookstore.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<ITemplateRenderer, TemplateRenderer>();
        services.AddSingleton<EmailTemplateDefinitionProvider>();

        return services;
    }
}