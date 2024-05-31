using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.DiscountAggregate;


public sealed class DiscountQuantityRange : ValueObject
{
    public int? MinApplyQuantity { get; private set; }
    public int? MaxApplyQuantity { get; private set; }

    private DiscountQuantityRange(int? minApplyQuantity, int? maxApplyQuantity)
    {
        MinApplyQuantity = minApplyQuantity;
        MaxApplyQuantity = maxApplyQuantity;
    }


    public static Result<DiscountQuantityRange> Create(int? minApplyQuantity, int? maxApplyQuantity)
    {
        if (minApplyQuantity > maxApplyQuantity)
        {
            return Result.Failure<DiscountQuantityRange>(DiscountVoucherError.InvalidApplyQuantityRange);
        }

        return Result.Success(new DiscountQuantityRange(minApplyQuantity, maxApplyQuantity));
    }

    public bool IsValidQuantity(int quantity)
    {
        if (quantity < MinApplyQuantity || quantity > MaxApplyQuantity)
        {
            return false;
        }

        return true;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return MinApplyQuantity;
        yield return MaxApplyQuantity;
    }
}
