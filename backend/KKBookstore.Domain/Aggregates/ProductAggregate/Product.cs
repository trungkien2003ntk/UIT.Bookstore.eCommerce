using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductAggregate;

public class Product : BaseAuditableEntity, ISoftDelete
{
    public Product() : base()
    {
        IsActive = true;
        IsDeleted = false;
    }

    private Product(
        string name,
        int productTypeId,
        string description,
        bool isBook = false,
        bool isActive = true
    ) : base()
    {
        Name = name;
        ProductTypeId = productTypeId;
        Description = description;
        IsBook = isBook;
        IsActive = isActive;
        IsDeleted = false;
    }

    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public string Description { get; set; }
    public bool IsBook { get; set; }
    public bool IsActive { get; set; }
    public int UnitMeasureId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation properties
    public ProductType ProductType { get; set; }
    public UnitMeasure UnitMeasure { get; set; }
    public ICollection<ProductOption> Options { get; set; } = [];
    public ICollection<BookAuthor> BookAuthors { get; set; } = [];
    public ICollection<Sku> Skus { get; set; } = [];
    public ICollection<Rating> Ratings { get; set; } = [];
    public ICollection<ProductImage> ProductImages { get; set; } = [];

    public static Result<Product> Create(
        string name,
        int productTypeId,
        string description,
        bool isBook = false,
        bool isActive = true
    )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Product>(ProductErrors.ProductNameRequired);
        }

        return new Product(name, productTypeId, description, isBook, isActive);
    }

    public IDictionary<string, IEnumerable<ProductOptionValue>> GetAllOptionsWithValues()
    {
        var optionsWithValues = new Dictionary<string, IEnumerable<ProductOptionValue>>();

        foreach (var option in Options)
        {
            optionsWithValues.Add(option.Name, option.OptionValues);
        }

        return optionsWithValues;
    }

    public string GetFirstThumbnailImageUrl()
    {
        return ProductImages.FirstOrDefault()?.ThumbnailImageUrl ?? "";
    }
}
