namespace KKBookstore.Application.Features.Images.UploadImages;

public record UploadImagesResponse
{
    public List<string> ImageUrls { get; init; } = [];
}