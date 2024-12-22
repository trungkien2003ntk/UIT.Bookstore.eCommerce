using KKBookstore.Domain.Products;
using KKBookstore.Domain.Shared.Products;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Products;

internal class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable("ProductVariants");
        builder.ConfigureAuditing();

        builder.Property(t => t.ProductId).HasColumnName(nameof(ProductVariant.ProductId)).IsRequired();
        builder.Property(t => t.Barcode).HasColumnName(nameof(ProductVariant.Barcode)).HasMaxLength(ProductVariantConsts.BarcodeMaxLength).IsRequired();
        builder.Property(t => t.RecommendedRetailPrice).HasColumnName(nameof(ProductVariant.RecommendedRetailPrice)).HasPrecision(18, 2).IsRequired();
        builder.Property(t => t.UnitPrice).HasColumnName(nameof(ProductVariant.UnitPrice)).HasPrecision(18, 2).IsRequired();
        builder.Property(t => t.TaxRate).HasColumnName(nameof(ProductVariant.TaxRate)).HasPrecision(18, 2);
        builder.Property(t => t.Weight).HasColumnName(nameof(ProductVariant.Weight)).HasPrecision(18, 2).IsRequired();
        builder.Property(t => t.Comment).HasColumnName(nameof(ProductVariant.Comment)).HasMaxLength(ProductVariantConsts.CommentMaxLength);

        // Value Objects
        builder.OwnsOne(t => t.SkuValue).Property(sv => sv.Value).HasMaxLength(ProductVariantConsts.SkuMaxLength).IsRequired();
        builder.OwnsOne(t => t.Dimension).Property(d => d.Length).HasPrecision(18, 2).IsRequired();
        builder.OwnsOne(t => t.Dimension).Property(d => d.Width).HasPrecision(18, 2).IsRequired();
        builder.OwnsOne(t => t.Dimension).Property(d => d.Height).HasPrecision(18, 2).IsRequired();

        // Foreign Key
        builder.HasOne(t => t.Product).WithMany(t => t.ProductVariants).HasForeignKey(t => t.ProductId);
    }
}
