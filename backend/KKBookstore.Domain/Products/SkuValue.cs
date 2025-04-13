using KKBookstore.Models;

namespace KKBookstore.Products;

public class SkuValue : ValueObject
{
    public SkuValue()
    {
        Value = "";
    }
    public SkuValue(string? value)
    {
        Value = value ?? string.Empty;
    }

    public string Value { get; set; }


    public static Result<SkuValue> Create(int productId)
    {
        string value = $"{productId}-{Guid.NewGuid()}";

        return new SkuValue(value);
    }

    public static Result<SkuValue> Create(string sku)
    {
        string value = sku;

        return new SkuValue(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
