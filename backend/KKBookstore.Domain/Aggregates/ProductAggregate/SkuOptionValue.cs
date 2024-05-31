using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductAggregate;

public class SkuOptionValue : BaseAuditableEntity, ISoftDelete, ITrackable
{
    public SkuOptionValue()
    {

    }
    private SkuOptionValue(
        int skuId,
        int optionId,
        int optionValueId
    ) : base()
    {
        SkuId = skuId;
        OptionId = optionId;
        OptionValueId = optionValueId;
        IsDeleted = false;
    }

    public int SkuId { get; set; }
    public int OptionId { get; set; }
    public int OptionValueId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation properties
    public ProductOption Option { get; set; }
    public Sku Sku { get; set; }
    public ProductOptionValue OptionValue { get; set; }

    public static Result<SkuOptionValue> Create(
        int skuId,
        int optionId,
        int optionValueId
    )
    {
        return new SkuOptionValue(skuId, optionId, optionValueId);
    }
}
