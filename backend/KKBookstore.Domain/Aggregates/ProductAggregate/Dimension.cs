using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductAggregate;

public class Dimension : ValueObject
{

    public Dimension()
    {

    }

    private Dimension(decimal Length, decimal Width, decimal Height)
    {
        this.Length = Length;
        this.Width = Width;
        this.Height = Height;
    }

    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

    public static Result<Dimension> Create(decimal Length, decimal Width, decimal Height)
    {
        if (Length <= 0)
            return Result.Failure<Dimension>(ProductErrors.NegativeLength);
        if (Width <= 0)
            return Result.Failure<Dimension>(ProductErrors.NegativeWidth);
        if (Height <= 0)
            return Result.Failure<Dimension>(ProductErrors.NegativeHeight);

        return new Dimension(Length, Width, Height);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Length;
        yield return Width;
        yield return Height;
    }
}
