using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;


// configuration class for Author entity
internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        // config the name of this table to nameof(Author)
        builder.ToTable($"{nameof(Author)}s");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(Author)}Id");

        builder.Property(t => t.Name);

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

        builder.ConfigureAuditing();
    }
}
