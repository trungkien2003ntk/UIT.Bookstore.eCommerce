using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ProductAggregate;

public class SkuImage : BaseAuditableEntity, ISoftDelete
{
    public SkuImage()
    {
        
    }
    private SkuImage(
        int skuId,
        string thumbnailImageUrl,
        string largeImageUrl
    ) : base()
    {
        SkuId = skuId;
        ThumbnailImageUrl = thumbnailImageUrl;
        LargeImageUrl = largeImageUrl;
        IsDeleted = false;
    }

    public int SkuId { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string LargeImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation properties
    public Sku Sku { get; set; }

    public static Result<SkuImage> Create(
        int skuId,
        string thumbnailImageUrl,
        string largeImageUrl
    )
    {
        if (string.IsNullOrWhiteSpace(thumbnailImageUrl) || string.IsNullOrWhiteSpace(largeImageUrl))
            return Result.Failure<SkuImage>(ProductErrors.ProductImageUrlRequired);

        return new SkuImage(skuId, thumbnailImageUrl, largeImageUrl);
    }
}
