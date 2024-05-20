using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.ProductAggregate;

public class ProductTypeAttributeProductValue : BaseAuditableEntity
{
    public int ProductTypeAttributeValueId { get; set; }
    public ProductTypeAttributeValue ProductTypeAttributeValue { get; set; }
    public int ProductId { get; set; } 
    public Product Product { get; set; }

}
