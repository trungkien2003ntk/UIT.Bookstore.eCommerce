using KKBookstore.Data.Extensions;
using KKBookstore.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Products;

internal class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        builder.ToTable("BookAuthors");
        builder.ConfigureAuditing();

        builder.Property(wb => wb.WriteTime).HasColumnName(nameof(BookAuthor.WriteTime)).IsRequired();
        builder.Property(wb => wb.AuthorId).HasColumnName(nameof(BookAuthor.AuthorId)).IsRequired();
        builder.Property(wb => wb.BookId).HasColumnName(nameof(BookAuthor.BookId)).IsRequired();

        builder.HasIndex(wb => new { wb.BookId, wb.AuthorId }).IsUnique();

        builder.HasOne(wb => wb.Book).WithMany(p => p.BookAuthors).HasForeignKey(wb => wb.BookId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(wb => wb.Author).WithMany(a => a.AuthorBooks).HasForeignKey(wb => wb.AuthorId).OnDelete(DeleteBehavior.Restrict);
    }
}
