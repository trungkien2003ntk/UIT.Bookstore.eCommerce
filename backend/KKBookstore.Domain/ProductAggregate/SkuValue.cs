using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.ProductAggregate;

public class SkuValue : ValueObject
{
    private SkuValue(string value)
    {
        Value = value;
    }

    public string Value { get; }


    public static Result<SkuValue> Create(int productId)
    {
        string value = $"{productId}-{Guid.NewGuid()}";

        return new SkuValue(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
