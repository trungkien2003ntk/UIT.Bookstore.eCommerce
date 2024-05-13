using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;

namespace KKBookstore.Domain.ProductAggregate;

public class Option : BaseAuditableEntity, ISoftDelete
{
    private Option(
        int productId,
        string name
    ) : base()
    {
        ProductId = productId;
        Name = name;
        IsDeleted = false;
    }

    public int ProductId { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation properties
    public Product Product { get; set; }
    public ICollection<OptionValue> OptionValues { get; set; } = new List<OptionValue>();

    public static Result<Option> Create(int productId, string name)
    {
        return new Option(
            productId,
            name
        );
    }
}
