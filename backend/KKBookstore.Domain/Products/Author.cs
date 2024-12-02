using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public class Author : BaseFullAuditedEntity
{
    public Author() { }

    private Author(
        string name,
        string description
        ) : base()
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }

    // navigation propery
    public ICollection<BookAuthor> AuthorBooks { get; set; } = new List<BookAuthor>();

    public static Result<Author> Create(string name, string description)
    {
        return new Author(name, description);
    }
}
