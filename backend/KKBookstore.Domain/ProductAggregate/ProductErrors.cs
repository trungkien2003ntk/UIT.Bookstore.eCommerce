using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.ProductAggregate;

public static class ProductErrors
{
    public static readonly Error NotFound = Error.NotFound("Product.NotFound", "Product was not found.");
    public static readonly Error ProductNameRequired = Error.Validation("Product.ProductNameRequired", "A product name is required.");
    
    public static readonly Error ProductOptionValueRequired = Error.Validation("Product.ProductOptionValueRequired", "A product option value is required.");
    public static readonly Error ProductOptionNameRequired = Error.Validation("Product.ProductOptionNameRequired", "A product option name is required.");

    public static readonly Error UnitMeasureCodeRequired = Error.Validation("Product.UnitMeasureCodeRequired", "A unit measure code is required.");

    public static readonly Error ProductTypeCodeRequired = Error.Validation("Product.ProductTypeCodeRequired", "A product type code is required.");
    public static readonly Error ProductTypeDisplayNameRequired = Error.Validation("Product.ProductTypeDisplayNameRequired", "A product type display name is required.");

    public static readonly Error ProductImageUrlRequired = Error.Validation("Product.ProductImageUrlRequired", "A product image URL is required.");

    public static readonly Error NegativeHeight = Error.Validation("Product.NegativeHeight", "Height cannot be negative.");
    public static readonly Error NegativeWidth = Error.Validation("Product.NegativeWidth", "Width cannot be negative.");
    public static readonly Error NegativeLength = Error.Validation("Product.NegativeLength", "Length cannot be negative.");
}
