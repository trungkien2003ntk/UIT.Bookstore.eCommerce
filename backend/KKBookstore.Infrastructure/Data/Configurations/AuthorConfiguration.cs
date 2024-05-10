using KKBookstore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;


// configuration class for Author entity
internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        // set column name of key property in database to AuthorId instead of Id
        builder.Property(t => t.Name)
            .HasColumnName("AuthorId");

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.IsDeleted)
            .IsRequired();

        builder.HasMany(t => t.AuthorBooks)
            .WithOne(t => t.Author)
            .HasForeignKey(t => t.AuthorId);
    }
}
