using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;

namespace KKBookstore.Domain.ProductAggregate;

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
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<Sku> Skus { get; set; } = new List<Sku>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

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
}
