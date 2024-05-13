using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ProductAggregate;

public class UnitMeasure : BaseAuditableEntity, ISoftDelete
{
    private UnitMeasure(
        string name,
        string description
    ) : base()
    {
        Name = name;
        Description = description;
        IsDeleted = false;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    public static Result<UnitMeasure> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<UnitMeasure>(ProductErrors.UnitMeasureCodeRequired);

        return new UnitMeasure(name, description);
    }   
}
