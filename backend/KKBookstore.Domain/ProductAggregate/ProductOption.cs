using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;

namespace KKBookstore.Domain.ProductAggregate;

public class ProductOption : BaseAuditableEntity, ISoftDelete
{
    public ProductOption()
    {
        IsDeleted = false;
    }
    private ProductOption(
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
    public ICollection<ProductOptionValue> OptionValues { get; set; } = new List<ProductOptionValue>();

    public static Result<ProductOption> Create(int productId, string name)
    {
        return new ProductOption(
            productId,
            name
        );
    }
}
