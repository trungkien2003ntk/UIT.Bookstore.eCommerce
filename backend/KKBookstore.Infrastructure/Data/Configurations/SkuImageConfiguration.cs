using KKBookstore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class SkuImageConfiguration : IEntityTypeConfiguration<SkuImage>
{
    public void Configure(EntityTypeBuilder<SkuImage> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(SkuImage)}Id");

        builder.Property(t => t.SkuId)
            .IsRequired();

        builder.Property(t => t.ThumbnailImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(t => t.LargeImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne(t => t.Sku)
            .WithMany(t => t.SkuImages)
            .HasForeignKey(t => t.SkuId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
