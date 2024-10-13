using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductTypeAttributeConfiguration : IEntityTypeConfiguration<ProductTypeAttribute>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttribute> builder)
    {
        builder.ToTable($"{nameof(ProductTypeAttribute)}s");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName($"{nameof(ProductTypeAttribute)}Id");

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);


        builder.ConfigureAuditing();
    }
}