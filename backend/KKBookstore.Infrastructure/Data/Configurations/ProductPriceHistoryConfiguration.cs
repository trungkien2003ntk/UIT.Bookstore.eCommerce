using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductPriceHistoryConfiguration : IEntityTypeConfiguration<ProductPriceHistory>
{
    public void Configure(EntityTypeBuilder<ProductPriceHistory> builder)
    {
        builder.ToTable("ProductPriceHistories");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(ProductPriceHistory)}Id");

        builder.Property(t => t.RecommendedRetailPrice)
            .HasPrecision(18, 2);

        builder.Property(t => t.UnitPrice)
            .HasPrecision(18, 2);


        builder.ConfigureAuditing();
    }
}
