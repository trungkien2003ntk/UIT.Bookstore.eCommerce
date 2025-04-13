using KKBookstore.Domain.Models;
using KKBookstore.Domain.ProductTypes;

namespace KKBookstore.ProductTypes;

public class ProductTypeAttribute : BaseAuditedEntity
{
    public ProductTypeAttribute(string name)
    {
        Name = name;
    }

    public ProductTypeAttribute()
    {
        Name = "";
    }
    public string Name { get; set; }

    public ICollection<ProductTypeAttributeMapping> ProductTypes { get; set; } = [];
    public ICollection<ProductTypeAttributeValue> Values { get; set; } = [];
}
