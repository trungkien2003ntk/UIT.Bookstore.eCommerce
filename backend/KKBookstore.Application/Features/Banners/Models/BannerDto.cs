using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Banners.Models;

public record BannerDto : BaseAuditedDto
{
    public string Title { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public string? TargetUrl { get; init; }
    public int? ProductTypeId { get; init; }
    public string? ProductTypeName { get; init; }
    public bool IsActive { get; init; }
}
