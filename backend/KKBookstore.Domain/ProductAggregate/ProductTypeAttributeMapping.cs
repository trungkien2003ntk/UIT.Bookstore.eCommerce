using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.ProductAggregate;

public class ProductTypeAttributeMapping : BaseAuditableEntity
{
    public int ProductTypeId { get; set; }
    public int ProductAttributeId { get; set; }

    // navigation properties
    public ProductType ProductType { get; set; }
    public ProductTypeAttribute ProductAttribute { get; set; }
}
