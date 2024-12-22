using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Products.Models;

public sealed record ProductImageDto : BaseDto
{
    public string ThumbnailImageUrl { get; set; }
    public string LargeImageUrl { get; set; }
}