using KKBookstore.Data.Extensions;
using KKBookstore.ProductTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.ProductTypes;

internal class ProductTypeAttributeMappingConfiguration : IEntityTypeConfiguration<ProductTypeAttributeMapping>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttributeMapping> builder)
    {
        builder.ToTable("ProductTypeAttributeMappings");
        builder.ConfigureAuditing();

        builder.Property(ptam => ptam.ProductTypeId).HasColumnName(nameof(ProductTypeAttributeMapping.ProductTypeId)).IsRequired();
        builder.Property(ptam => ptam.ProductTypeAttributeId).HasColumnName(nameof(ProductTypeAttributeMapping.ProductTypeAttributeId)).IsRequired();

        builder.HasOne(x => x.ProductType).WithMany(y => y.Attributes).HasForeignKey(x => x.ProductTypeId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ProductTypeAttribute).WithMany(y => y.ProductTypes).HasForeignKey(x => x.ProductTypeAttributeId).OnDelete(DeleteBehavior.Restrict);
    }
}
