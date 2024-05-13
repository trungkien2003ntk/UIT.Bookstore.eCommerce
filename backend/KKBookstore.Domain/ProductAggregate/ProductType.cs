using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Helpers;
using KKBookstore.Domain.Common.Interfaces;

namespace KKBookstore.Domain.ProductAggregate;


public class ProductType : BaseAuditableEntity, ISoftDelete
{
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
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    public int? ParentProductTypeId { get; set; }

    // navigation property
    public ProductType? ParentProductType { get; set; }

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
