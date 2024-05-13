using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.OrderAggregate;

public class OrderNumber : ValueObject
{
    private OrderNumber(
        string value
    ): base()
    {
        Value = value;    
    }

    public string Value { get; }


    public static Result<OrderNumber> Create(DateTimeOffset OrderWhen)
    {
        string value = $"SO-{OrderWhen.Year}{OrderWhen.Month}{OrderWhen.Day}-{Guid.NewGuid().ToString()[..5]}";

        return new OrderNumber(value);
    }


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }


}
