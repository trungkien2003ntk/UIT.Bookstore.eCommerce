using KKBookstore.Domain.ProductTypes;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.ProductTypes;

internal class ProductTypeAttributeMappingConfiguration : IEntityTypeConfiguration<ProductTypeAttributeMapping>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttributeMapping> builder)
    {
        builder.ToTable("ProductTypeAttributeMappings");
        builder.ConfigureAuditing();

        builder.Property(ptam => ptam.ProductTypeId).HasColumnName(nameof(ProductTypeAttributeMapping.ProductTypeId)).IsRequired();
        builder.Property(ptam => ptam.ProductAttributeId).HasColumnName(nameof(ProductTypeAttributeMapping.ProductAttributeId)).IsRequired();

        builder.HasOne(x => x.ProductType).WithMany(y => y.Attributes).HasForeignKey(x => x.ProductTypeId);
        builder.HasOne(x => x.ProductAttribute).WithMany(y => y.ProductTypes).HasForeignKey(x => x.ProductAttributeId);
    }
}
