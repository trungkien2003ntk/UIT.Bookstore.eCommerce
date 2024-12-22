using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public static class ProductErrors
{
    public static readonly Error NotFound = Error.NotFound("Product.NotFound", "Product was not found.");
    public static readonly Error RatingNotFound = Error.NotFound("Product.RatingNotFound", "Product rating was not found.");
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

    public static readonly Error CreateProductFailed = Error.Failure("Product.CreateProductFailed", "Failed to create product.");

    public static Error AttributeNotFound(List<int> missingIds)
    {
        return Error.NotFound("Product.AttributeNotFound", $"Attributes with ids [{string.Join(",", missingIds)}] were not found.");
    }

    public static Error InvalidAttribute(string attributeName)
    {
        return Error.Validation("Product.InvalidAttribute", $"Invalid attribute: {attributeName}");
    }

    public static Error InvalidAttributeValue(string attributeName, IEnumerable<string> validAttributeValues)
    {
        return Error.Validation("Product.InvalidAttributeValue", $"Invalid value for attribute: {attributeName}, valid values: [{string.Join(",", validAttributeValues)}]");
    }

    public static Error ProductAlreadyExists(string name)
    {
        return Error.Conflict("Product.ProductAlreadyExists", $"Product with name '{name}' already exists.");
    }
}
