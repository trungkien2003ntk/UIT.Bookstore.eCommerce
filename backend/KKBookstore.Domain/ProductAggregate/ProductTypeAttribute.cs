using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.ProductAggregate;

public class ProductTypeAttribute : BaseAuditableEntity
{
    public string Name { get; set; }
    public ICollection<ProductTypeAttributeMapping> ProductTypes { get; set; } = new List<ProductTypeAttributeMapping>();
    public ICollection<ProductTypeAttributeValue> Values { get; set; } = new List<ProductTypeAttributeValue>();
}
