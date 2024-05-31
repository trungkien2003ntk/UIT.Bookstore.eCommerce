using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductTypeAttributeMappingConfiguration : IEntityTypeConfiguration<ProductTypeAttributeMapping>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttributeMapping> builder)
    {
        builder.ToTable($"{nameof(ProductTypeAttributeMapping)}s");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName($"{nameof(ProductTypeAttributeMapping)}Id");

        builder.HasOne(x => x.ProductType)
            .WithMany(y => y.Attributes)
            .HasForeignKey(x => x.ProductTypeId);

        builder.HasOne(x => x.ProductAttribute)
            .WithMany(y => y.ProductTypes)
            .HasForeignKey(x => x.ProductAttributeId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
