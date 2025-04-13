using KKBookstore.Domain.Models;
using KKBookstore.Domain.ProductTypes;

namespace KKBookstore.ProductTypes;

public class ProductTypeAttributeValue : BaseAuditedEntity
{
    public ProductTypeAttributeValue()
    {
        Value = "";
    }
    public string Value { get; set; }
    public int ProductTypeAttributeId { get; set; }

    public ProductTypeAttribute ProductTypeAttribute { get; set; } = null!;
    public ICollection<ProductTypeAttributeProductValue> ProductsAppliedValue { get; set; } = [];
}
