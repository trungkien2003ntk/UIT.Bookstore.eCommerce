namespace KKBookstore.Features.Images.UploadImages;

public record UploadImagesResponse
{
    public List<string> ImageUrls { get; init; } = [];
}