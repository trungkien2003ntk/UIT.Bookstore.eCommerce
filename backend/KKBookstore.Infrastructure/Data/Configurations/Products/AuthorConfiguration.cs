using KKBookstore.Domain.Products;
using KKBookstore.Domain.Shared.Products;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Products;


// configuration class for Author entity
internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> b)
    {
        // config the name of this table to nameof(Author)
        b.ToTable("Authors");
        b.ConfigureAuditing();

        b.Property<string>(nameof(Author.Name)).HasColumnName(nameof(Author.Name)).HasMaxLength(AuthorConsts.NameMaxLength).IsRequired();
        b.Property(t => t.Description).HasColumnName(nameof(Author.Description)).HasMaxLength(AuthorConsts.DescriptionMaxLength);

        b.HasMany(t => t.AuthorBooks).WithOne(t => t.Author).HasForeignKey(t => t.AuthorId);
    }
}
