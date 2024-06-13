namespace KKBookstore.Infrastructure.Email;

internal class EmailTemplate
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public bool IsHtml { get; set; }
}
