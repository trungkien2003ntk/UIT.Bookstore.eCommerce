using KKBookstore.Domain.Helpers;
using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;

namespace KKBookstore.Domain.ProductTypes;


public class ProductType : BaseFullAuditedEntity
{
    public ProductType()
    {

    }
    private ProductType(
        string productTypeCode,
        string displayName,
        string description,
        int level,
        int? parentProductTypeId
    ) : base()
    {
        ProductTypeCode = productTypeCode;
        DisplayName = displayName;
        Description = description;
        Level = level;
        ParentProductTypeId = parentProductTypeId;
        IsDeleted = false;
    }

    public string ProductTypeCode { get; set; }
    public int Level { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public int? ParentProductTypeId { get; set; }

    // navigation property
    public ProductType? ParentProductType { get; set; }
    public ICollection<ProductTypeAttributeMapping> Attributes { get; set; } = [];

    public static Result<ProductType> Create(
        string displayName,
        string description,
        int level = 0,
        int? parentProductTypeId = null
    )
    {
        if (string.IsNullOrWhiteSpace(displayName))
        {
            return Result.Failure<ProductType>(ProductErrors.ProductTypeDisplayNameRequired);
        }

        // display name can be vietnamese, so normalize it and replace space with dash
        var productTypeCode = StringHelper.RemoveDiacritics(displayName).Replace(" ", "-").ToLower();

        return new ProductType(productTypeCode, displayName, description, level, parentProductTypeId);
    }
}
