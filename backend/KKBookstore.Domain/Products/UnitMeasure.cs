using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public class UnitMeasure : BaseFullAuditedEntity
{
    public UnitMeasure()
    {

    }
    private UnitMeasure(
        string name,
        string description
    ) : base()
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public static Result<UnitMeasure> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<UnitMeasure>(ProductErrors.UnitMeasureCodeRequired);

        return new UnitMeasure(name, description);
    }
}
