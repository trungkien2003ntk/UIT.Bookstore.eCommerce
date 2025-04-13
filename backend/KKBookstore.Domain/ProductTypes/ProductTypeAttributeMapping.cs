using KKBookstore.Domain.Models;
using KKBookstore.Domain.ProductTypes;

namespace KKBookstore.ProductTypes;

public class ProductTypeAttributeMapping : BaseAuditedEntity
{
    public int ProductTypeId { get; set; }
    public int ProductTypeAttributeId { get; set; }

    // navigation properties
    public ProductType ProductType { get; set; } = null!;
    public ProductTypeAttribute ProductTypeAttribute { get; set; } = null!;
}
