namespace KKBookstore.Application.Common.Models;

public class BlobMetadata
{
    public string FileName { get; set; } = null!;
    public long Size { get; set; }
    public string ContentType { get; set; } = null!;
    public DateTime LastModified { get; set; }
}