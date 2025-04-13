namespace KKBookstore.Emailing;

public interface ITemplateRenderer
{
    Task<string> RenderTemplateAsync(string templateName, object? model = null);
}
