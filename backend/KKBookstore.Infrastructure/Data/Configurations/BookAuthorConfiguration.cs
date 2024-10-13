using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        builder.ToTable($"{nameof(BookAuthor)}s");

        builder.Property(wb => wb.Id)
            .HasColumnName($"{nameof(BookAuthor)}Id");

        builder.Property(wb => wb.WrittenWhen)
            .IsRequired();

        builder.HasIndex(wb => new { wb.ProductId, wb.AuthorId })
            .IsUnique();

        builder.HasOne(wb => wb.Product)
            .WithMany(p => p.BookAuthors)
            .HasForeignKey(wb => wb.ProductId);

        builder.HasOne(wb => wb.Author)
            .WithMany(a => a.AuthorBooks)
            .HasForeignKey(wb => wb.AuthorId);


        builder.ConfigureAuditing();
    }
}
