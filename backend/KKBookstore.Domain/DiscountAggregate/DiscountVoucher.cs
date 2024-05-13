using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.ProductAggregate;

namespace KKBookstore.Domain.DiscountAggregate;

public class DiscountVoucher : BaseAuditableEntity, ISoftDelete
{
    protected DiscountVoucher() : base() { }

    private DiscountVoucher(
        string name,
        string description,
        bool isPercentage,
        DiscountType discountType,
        DiscountQuantityRange quantityRange,
        DiscountValueRange valueRange,
        DateTimeOffset startWhen,
        DateTimeOffset endWhen,
        int value,
        int? productId
        ) : base()
    {
        Name = name;
        Description = description;
        IsPercentage = isPercentage;
        DiscountType = discountType;
        QuantityRange = quantityRange;
        ValueRange = valueRange;
        StartWhen = startWhen;
        EndWhen = endWhen;
        Value = value;
        ProductId = productId;
        IsDeleted = false;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPercentage { get; set; }
    public DiscountType DiscountType { get; set; }
    public DiscountQuantityRange QuantityRange { get; set; }
    public DiscountValueRange ValueRange { get; set; }
    public int Value { get; set; }
    public DateTimeOffset StartWhen { get; set; }
    public DateTimeOffset EndWhen { get; set; }
    public int? ProductId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation property to Order and OrderLine
    public Product? Product { get; set; }

    public bool IsValidForProduct(Product product)
    {
        if (ProductId == null)
        {
            return true;
        }

        return ProductId == product.Id;
    }

    public static Result<DiscountVoucher> Create(
         string name,
        string description,
        bool isPercentage,
        DiscountType discountType,
        int? minApplyQuantity,
        int? maxApplyQuantity,
        decimal? minValue,
        decimal maxValue,
        DateTimeOffset startWhen,
        DateTimeOffset endWhen,
        int value,
        int? productId = null
        )
    {
        // validation logic
        var quantityRangeResult = DiscountQuantityRange.Create(minApplyQuantity, maxApplyQuantity);
        if (quantityRangeResult.IsFailure)
        {
            return Result.Failure<DiscountVoucher>(quantityRangeResult.Error);
        }

        var valueRangeResult = DiscountValueRange.Create(maxValue, minValue != null ? minValue.Value : 0);
        if (valueRangeResult.IsFailure)
        {
            return Result.Failure<DiscountVoucher>(valueRangeResult.Error);
        }


        return Result.Success(new DiscountVoucher(
            name,
            description,
            isPercentage,
            discountType,
            quantityRangeResult.Value,
            valueRangeResult.Value,
            startWhen,
            endWhen,
            value,
            productId
        ));
    }
}
