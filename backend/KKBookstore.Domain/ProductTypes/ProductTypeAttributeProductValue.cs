using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;
using KKBookstore.Domain.ProductTypes;

namespace KKBookstore.ProductTypes;

public class ProductTypeAttributeProductValue : BaseAuditedEntity
{
    public int AttributeValueId { get; set; }
    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;
    public ProductTypeAttributeValue AttributeValue { get; set; } = null!;
}
