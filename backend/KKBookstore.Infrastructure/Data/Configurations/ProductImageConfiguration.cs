using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable($"{nameof(ProductImage)}s");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName($"{nameof(ProductImage)}Id");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.ThumbnailImageUrl)
            .HasColumnType("varchar(MAX)")
            .IsRequired();

        builder.Property(e => e.LargeImageUrl)
            .HasColumnType("varchar(MAX)")
            .IsRequired();

        builder.Property(e => e.ProductId)
            .IsRequired();

        builder.ConfigureAuditing();

        builder.HasOne(e => e.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
