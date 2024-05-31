using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductAggregate;

public class BookAuthor : BaseAuditableEntity, ISoftDelete
{
    public BookAuthor()
    {

    }

    private BookAuthor(
        int authorId,
        int productId,
        DateTimeOffset writtenWhen
    ) : base()
    {
        AuthorId = authorId;
        ProductId = productId;
        WrittenWhen = writtenWhen;
        IsDeleted = false;
    }

    public int AuthorId { get; set; }
    public int ProductId { get; set; }
    public DateTimeOffset WrittenWhen { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    public Author Author { get; set; }
    public Product Product { get; set; }

    public static Result<BookAuthor> Create(
        int authorId,
        int productId,
        DateTimeOffset writtenWhen
    )
    {
        return new BookAuthor(authorId, productId, writtenWhen);
    }
}
