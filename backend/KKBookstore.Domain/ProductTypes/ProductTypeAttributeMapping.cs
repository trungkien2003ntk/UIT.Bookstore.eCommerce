using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.ProductTypes;

public class ProductTypeAttributeMapping : BaseAuditedEntity
{
    public int ProductTypeId { get; set; }
    public int ProductAttributeId { get; set; }

    // navigation properties
    public ProductType ProductType { get; set; } = null!;
    public ProductTypeAttribute ProductAttribute { get; set; } = null!;
}
