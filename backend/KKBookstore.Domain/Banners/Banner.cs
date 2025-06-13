using KKBookstore.Models;
using KKBookstore.ProductTypes;

namespace KKBookstore.Banners;

public class Banner : BaseAuditedEntity
{
    public Banner()
    {
    }

    private Banner(
        string title,
        string imageUrl,
        string? targetUrl,
        int? productTypeId,
        bool isActive
    ) : base()
    {
        Title = title;
        ImageUrl = imageUrl;
        TargetUrl = targetUrl;
        ProductTypeId = productTypeId;
        IsActive = isActive;
    }

    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string? TargetUrl { get; set; }
    public int? ProductTypeId { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public ProductType? ProductType { get; set; }

    // Domain methods
    public Result Activate()
    {
        if (IsActive)
            return Result.Failure(BannerErrors.BannerAlreadyActive);

        IsActive = true;
        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure(BannerErrors.BannerAlreadyInactive);

        IsActive = false;
        return Result.Success();
    }

    public Result ToggleStatus()
    {
        IsActive = !IsActive;
        return Result.Success();
    }

    public Result UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure(BannerErrors.TitleRequired);
        }

        Title = title;
        return Result.Success();
    }

    public Result UpdateImageUrl(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            return Result.Failure(BannerErrors.ImageUrlRequired);
        }

        ImageUrl = imageUrl;
        return Result.Success();
    }

    public Result UpdateTargetUrl(string? targetUrl)
    {
        TargetUrl = targetUrl;
        return Result.Success();
    }

    // Factory method
    public static Result<Banner> Create(
        string title,
        string imageUrl,
        string? targetUrl = null,
        int? productTypeId = null,
        bool isActive = true
    )
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<Banner>(BannerErrors.TitleRequired);
        }

        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            return Result.Failure<Banner>(BannerErrors.ImageUrlRequired);
        }

        var banner = new Banner(
            title,
            imageUrl,
            targetUrl,
            productTypeId,
            isActive
        );

        return Result.Success(banner);
    }
}
