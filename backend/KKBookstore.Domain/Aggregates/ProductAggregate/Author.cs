using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductAggregate;

public class Author : BaseAuditableEntity, ISoftDelete
{
    public Author() { }

    private Author(
        string name,
        string description
        ) : base()
    {
        Name = name;
        Description = description;
        IsDeleted = false;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation propery
    public ICollection<BookAuthor> AuthorBooks { get; set; } = new List<BookAuthor>();

    public static Result<Author> Create(string name, string description)
    {
        return new Author(name, description);
    }
}
