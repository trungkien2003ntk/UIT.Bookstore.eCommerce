using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public class ProductVariantOptionValue : BaseFullAuditedEntity
{
    public ProductVariantOptionValue()
    {

    }
    private ProductVariantOptionValue(
        int productVariantId,
        int optionId,
        int optionValueId
    ) : base()
    {
        ProductVariantId = productVariantId;
        OptionId = optionId;
        OptionValueId = optionValueId;
    }

    public int ProductVariantId { get; set; }
    public int OptionId { get; set; }
    public int OptionValueId { get; set; }

    // navigation properties
    public ProductOption Option { get; set; }
    public ProductVariant ProductVariant { get; set; }
    public ProductOptionValue OptionValue { get; set; }

    public static Result<ProductVariantOptionValue> Create(
        int productVariantId,
        int optionId,
        int optionValueId
    )
    {
        return new ProductVariantOptionValue(productVariantId, optionId, optionValueId);
    }
}
