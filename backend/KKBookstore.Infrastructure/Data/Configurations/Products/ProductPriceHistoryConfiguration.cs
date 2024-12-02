using KKBookstore.Domain.Products;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Products;

internal class ProductPriceHistoryConfiguration : IEntityTypeConfiguration<ProductPriceHistory>
{
    public void Configure(EntityTypeBuilder<ProductPriceHistory> builder)
    {
        builder.ToTable("ProductPriceHistories");
        builder.ConfigureAuditing();

        builder.Property(t => t.RecommendedRetailPrice).HasColumnName(nameof(ProductPriceHistory.RecommendedRetailPrice)).HasPrecision(18, 2);
        builder.Property(t => t.UnitPrice).HasColumnName(nameof(ProductPriceHistory.UnitPrice)).HasPrecision(18, 2);
        builder.Property(t => t.StartTime).HasColumnName(nameof(ProductPriceHistory.StartTime)).IsRequired();
        builder.Property(t => t.EndTime).HasColumnName(nameof(ProductPriceHistory.EndTime)).IsRequired();
        builder.Property(t => t.ProductVariantId).HasColumnName(nameof(ProductPriceHistory.ProductVariantId)).IsRequired();

        builder.Ignore(nameof(ProductPriceHistory.ProductVariant));
    }
}
