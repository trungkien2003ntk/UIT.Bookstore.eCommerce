using KKBookstore.Data.Extensions;
using KKBookstore.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Products;

internal class ProductVariantOptionValueConfiguration : IEntityTypeConfiguration<ProductVariantOptionValue>
{
    public void Configure(EntityTypeBuilder<ProductVariantOptionValue> builder)
    {
        builder.ToTable("ProductVariantOptionValues");
        builder.ConfigureAuditing();

        builder.HasIndex(t => new { t.ProductVariantId, t.OptionId, t.OptionValueId }).IsUnique();
        builder.Property(t => t.ProductVariantId).IsRequired();
        builder.Property(t => t.OptionId).IsRequired();
        builder.Property(t => t.OptionValueId).IsRequired();

        builder.HasOne(t => t.ProductVariant).WithMany(t => t.ProductVariantOptionValues).HasForeignKey(t => t.ProductVariantId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Option).WithMany().HasForeignKey(t => t.OptionId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.OptionValue).WithMany().HasForeignKey(t => t.OptionValueId).OnDelete(DeleteBehavior.Restrict);
    }
}
