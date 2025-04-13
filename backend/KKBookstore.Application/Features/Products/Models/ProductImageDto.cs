using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Products.Models;

public sealed record ProductImageDto : BaseDto
{
    public string ThumbnailImageUrl { get; set; }
    public string LargeImageUrl { get; set; }
}