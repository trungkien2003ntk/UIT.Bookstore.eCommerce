using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductTypeAggregate;

public class ProductTypeAttributeValue : BaseAuditableEntity
{
    /// <summary>
    /// TODO: config this run migration
    /// </summary>
    public string Value { get; set; }
    public int ProductTypeAttributeId { get; set; }
    public ProductTypeAttribute ProductTypeAttribute { get; set; }
    public ICollection<ProductTypeAttributeProductValue> ProductsAppliedValue { get; set; } =
        new List<ProductTypeAttributeProductValue>();
}
