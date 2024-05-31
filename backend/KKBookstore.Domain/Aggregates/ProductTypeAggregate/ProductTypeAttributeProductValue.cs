using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductTypeAggregate;

public class ProductTypeAttributeProductValue : BaseAuditableEntity
{
    public int AttributeValueId { get; set; }
    public ProductTypeAttributeValue AttributeValue { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }

}
