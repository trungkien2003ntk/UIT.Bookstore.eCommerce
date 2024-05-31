using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductTypeAggregate;

public class ProductTypeAttribute : BaseAuditableEntity
{
    public string Name { get; set; }
    public ICollection<ProductTypeAttributeMapping> ProductTypes { get; set; } = new List<ProductTypeAttributeMapping>();
    public ICollection<ProductTypeAttributeValue> Values { get; set; } = new List<ProductTypeAttributeValue>();
}
