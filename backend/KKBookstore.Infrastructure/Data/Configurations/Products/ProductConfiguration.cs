using KKBookstore.Domain.Products;
using KKBookstore.Domain.Shared.Products;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Products;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.ConfigureAuditing();

        builder.Property(t => t.Name).HasColumnName(nameof(Product.Name)).HasMaxLength(ProductConsts.NameMaxLength).IsRequired();
        builder.Property(t => t.Description).HasColumnName(nameof(Product.Description)).HasMaxLength(ProductConsts.DescriptionMaxLength);

        builder.Property(t => t.ProductTypeId).HasColumnName(nameof(Product.ProductTypeId)).IsRequired();
        builder.Property(t => t.UnitMeasureId).HasColumnName(nameof(Product.UnitMeasureId)).IsRequired();
        builder.Property(t => t.IsBook).HasColumnName(nameof(Product.IsBook)).IsRequired();
        builder.Property(t => t.IsActive).HasColumnName(nameof(Product.IsActive)).IsRequired();
        
        builder.HasIndex(t => t.Name).IsUnique();
        builder.HasIndex(t => t.ProductTypeId);

        builder.HasOne(t => t.ProductType).WithMany().HasForeignKey(t => t.ProductTypeId);
        builder.HasOne(t => t.UnitMeasure).WithMany().HasForeignKey(t => t.UnitMeasureId);
    }
}
