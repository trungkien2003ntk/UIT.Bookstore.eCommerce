using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public class ProductImage : BaseFullAuditedEntity
{
    public ProductImage()
    {

    }
    private ProductImage(
        int productId,
        string thumbnailImageUrl,
        string largeImageUrl
    ) : base()
    {
        ProductId = productId;
        ThumbnailImageUrl = thumbnailImageUrl;
        LargeImageUrl = largeImageUrl;
    }

    public int ProductId { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string LargeImageUrl { get; set; }

    // navigation properties
    public Product Product { get; set; }

    public static Result<ProductImage> Create(
        int productId,
        string thumbnailImageUrl,
        string largeImageUrl
    )
    {
        if (string.IsNullOrWhiteSpace(thumbnailImageUrl) || string.IsNullOrWhiteSpace(largeImageUrl))
            return Result.Failure<ProductImage>(ProductErrors.ProductImageUrlRequired);

        return new ProductImage(productId, thumbnailImageUrl, largeImageUrl);
    }
}
