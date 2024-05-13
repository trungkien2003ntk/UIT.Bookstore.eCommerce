using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ProductAggregate;

public class SkuOptionValue : BaseAuditableEntity, ISoftDelete, ITrackable
{
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
    public Option Option { get; set; }
    public Sku Sku { get; set; }
    public OptionValue OptionValue { get; set; }

    public static Result<SkuOptionValue> Create(
        int skuId,
        int optionId,
        int optionValueId
    )
    {
        return new SkuOptionValue(skuId, optionId, optionValueId);
    }
}
