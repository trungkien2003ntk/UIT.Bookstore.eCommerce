using Scriban;
using Scriban.Syntax;
using System.Collections.Concurrent;
using System.Reflection;

namespace KKBookstore.Domain.Emailing.Services;

public class TemplateRenderer : ITemplateRenderer
{
    private readonly EmailTemplateDefinitionProvider _templateDefinitionProvider;
    private readonly ConcurrentDictionary<string, Template> _templateCache = new();

    public TemplateRenderer(EmailTemplateDefinitionProvider templateDefinition)
    {
        _templateDefinitionProvider = templateDefinition;
    }

    public async Task<string> RenderTemplateAsync(string templateName, object? model = null)
    {
        try
        {
            var templateDefinition = _templateDefinitionProvider.GetTemplateDefinition(templateName);
            var template = await GetTemplateAsync(templateDefinition.Path);

            var newModel = new
            {
                model,
            };
            var body = template.Render(newModel);

            if (templateDefinition.Layout != null)
            {
                var layoutDefinition = _templateDefinitionProvider.GetTemplateDefinition(templateDefinition.Layout);
                var layoutTemplate = await GetTemplateAsync(layoutDefinition.Path);

                var layoutModel = new
                {
                    content = body
                };

                body = layoutTemplate.Render(layoutModel);
            }

            return body;
        }
        catch (ScriptRuntimeException ex)
        {
            throw new Exception($"Error rendering template '{templateName}': {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error parsing template '{templateName}': {ex.Message}", ex);
        }
    }

    private async Task<Template> GetTemplateAsync(string templatePath)
    {
        if (_templateCache.TryGetValue(templatePath, out var cachedTemplate))
        {
            return cachedTemplate;
        }

        var templateContent = await GetEmbeddedResourceContentAsync(templatePath);
        var parseResult = Template.ParseLiquid(templateContent);

        if (parseResult.HasErrors)
        {
            throw new Exception($"Failed to parse template: {string.Join(", ", parseResult.Messages)}");
        }

        _templateCache.TryAdd(templatePath, parseResult);
        return parseResult;
    }

    private async Task<string> GetEmbeddedResourceContentAsync(string resourcePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var manifestResourceNames = assembly.GetManifestResourceNames();
        var resourceName = manifestResourceNames.FirstOrDefault(r => r.EndsWith(resourcePath));

        if (resourceName == null)
        {
            throw new FileNotFoundException($"Embedded resource '{resourcePath}' not found.");
        }

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream!);
        return await reader.ReadToEndAsync();
    }
}


