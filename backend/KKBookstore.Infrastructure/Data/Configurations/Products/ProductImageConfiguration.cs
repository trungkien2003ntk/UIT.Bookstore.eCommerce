using KKBookstore.Domain.Products;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Products;

internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");
        builder.ConfigureAuditing();

        builder.Property(e => e.ThumbnailImageUrl).HasColumnName(nameof(ProductImage.ThumbnailImageUrl)).IsRequired();
        builder.Property(e => e.LargeImageUrl).HasColumnName(nameof(ProductImage.LargeImageUrl)).IsRequired();

        builder.Property(e => e.ProductId).HasColumnName(nameof(ProductImage.ProductId)).IsRequired();

        builder.HasOne(e => e.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
