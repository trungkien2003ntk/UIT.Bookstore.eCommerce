namespace KKBookstore.Domain.Emailing.Models;

[Serializable]
public class EmailAttachment
{
    public string? Name { get; set; }

    public byte[]? File { get; set; }
}