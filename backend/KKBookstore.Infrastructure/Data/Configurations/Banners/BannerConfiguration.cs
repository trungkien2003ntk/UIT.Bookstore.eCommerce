using KKBookstore.Banners;
using KKBookstore.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Banners;

internal class BannerConfiguration : IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> builder)
    {
        builder.ToTable("Banners");
        builder.ConfigureAuditing();

        builder.Property(b => b.Title)
            .HasColumnName(nameof(Banner.Title))
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.ImageUrl)
            .HasColumnName(nameof(Banner.ImageUrl))
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(b => b.TargetUrl)
            .HasColumnName(nameof(Banner.TargetUrl))
            .HasMaxLength(500);

        builder.Property(b => b.IsActive)
            .HasColumnName(nameof(Banner.IsActive))
            .IsRequired()
            .HasDefaultValue(true);

        // Configure relationship with ProductType
        builder.HasOne(b => b.ProductType)
            .WithMany()
            .HasForeignKey(b => b.ProductTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Add indexes for performance
        builder.HasIndex(b => b.IsActive);
        builder.HasIndex(b => b.ProductTypeId);
    }
}
