using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public class BookAuthor : BaseFullAuditedEntity
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
        BookId = productId;
        WriteTime = writtenWhen;
    }

    public int AuthorId { get; set; }
    public int BookId { get; set; }
    public DateTimeOffset WriteTime { get; set; }

    public Author Author { get; set; } = null!;
    public Product Book { get; set; } = null!;

    public static Result<BookAuthor> Create(
        int authorId,
        int bookId,
        DateTimeOffset writeTime
    )
    {
        return new BookAuthor(authorId, bookId, writeTime);
    }
}
