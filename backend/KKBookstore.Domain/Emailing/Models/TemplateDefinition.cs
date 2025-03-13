namespace KKBookstore.Domain.Emailing.Models;

public class TemplateDefinition
{
    public string Name { get; }
    public string? Layout { get; }
    public string Path { get; }
    public bool IsLayout { get; }

    public TemplateDefinition(string name, string path, bool isLayout = false, string? layout = null)
    {
        Name = name;
        Path = path;
        IsLayout = isLayout;
        Layout = layout;
    }
}
