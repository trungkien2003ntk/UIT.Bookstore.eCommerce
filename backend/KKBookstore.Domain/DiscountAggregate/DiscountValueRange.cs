using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.DiscountAggregate;

public sealed class DiscountValueRange : ValueObject
{
    public decimal MinValue { get; private set; }
    public decimal MaxValue { get; private set; }

    public DiscountValueRange(decimal minValue, decimal maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
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
