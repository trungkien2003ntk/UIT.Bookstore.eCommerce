using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.DiscountAggregate;

public sealed class DiscountValueRange : ValueObject
{
    private DiscountValueRange(
        decimal maxValue,
        decimal minValue = 0
    )
    {
        MaxValue = maxValue;
        MinValue = minValue;
    }

    public decimal MinValue { get; private set; } = 0;
    public decimal MaxValue { get; private set; }

    public static Result<DiscountValueRange> Create(decimal maxValue, decimal minValue = 0)
    {
        if (maxValue < minValue)
        {
            return Result.Failure<DiscountValueRange>(DiscountVoucherError.InvalidValueRange);
        }

        return new DiscountValueRange(maxValue, minValue);
    }

    public bool IsValidValue(decimal value)
    {
        if (value < MinValue || value > MaxValue)
        {
            return false;
        }

        return true;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return MinValue;
        yield return MaxValue;
    }
}
