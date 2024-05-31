using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductAggregate;

public class SkuValue : ValueObject
{
    public SkuValue()
    {

    }
    private SkuValue(string value)
    {
        Value = value;
    }

    public string Value { get; set; }


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
