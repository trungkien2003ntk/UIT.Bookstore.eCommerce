using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;

namespace KKBookstore.Domain.ProductTypes;

public class ProductTypeAttributeProductValue : BaseAuditedEntity
{
    public int AttributeValueId { get; set; }
    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;
    public ProductTypeAttributeValue AttributeValue { get; set; } = null!;
}
