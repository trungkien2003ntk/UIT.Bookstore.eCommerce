using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public class ProductOption : BaseFullAuditedEntity
{
    public ProductOption()
    {
    }
    private ProductOption(
        int productId,
        string name
    ) : base()
    {
        ProductId = productId;
        Name = name;
    }

    public int ProductId { get; set; }
    public string Name { get; set; }
    public bool IsOptionWithImage { get; set; }

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
