namespace KKBookstore.Contracts.Requests;

public class GetRelatedProductsByImageRequest
{
    public string Base64Image { get; set; } = string.Empty;
}
