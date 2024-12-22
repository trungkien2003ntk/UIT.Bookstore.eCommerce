using KKBookstore.Domain.Products;
using KKBookstore.Domain.Shared.Products;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Products;

internal class ProductOptionValueConfiguration : IEntityTypeConfiguration<ProductOptionValue>
{
    public void Configure(EntityTypeBuilder<ProductOptionValue> builder)
    {
        builder.ToTable("ProductOptionValues");
        builder.ConfigureAuditing();

        builder.Property(t => t.Value).HasColumnName(nameof(ProductOptionValue.Value)).HasMaxLength(ProductOptionValueConsts.ValueMaxLength).IsRequired();
        builder.Property(t => t.ThumbnailImageUrl).HasColumnName(nameof(ProductOptionValue.ThumbnailImageUrl));
        builder.Property(t => t.LargeImageUrl).HasColumnName(nameof(ProductOptionValue.LargeImageUrl));

        builder.HasOne(t => t.Option).WithMany(o => o.OptionValues).HasForeignKey(t => t.OptionId);
    }
}
