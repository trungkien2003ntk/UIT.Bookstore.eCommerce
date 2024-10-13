using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.ToTable($"{nameof(ProductType)}s");

        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(ProductType)}Id");

        builder.Property(t => t.DisplayName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.ProductTypeCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Level)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.IsDeleted)
            .IsRequired();

        builder.HasIndex(t => t.DisplayName)
            .IsUnique();


        builder.ConfigureAuditing();
    }
}

