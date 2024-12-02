using KKBookstore.Domain.Products;
using KKBookstore.Domain.Shared.Products;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Infrastructure.Data.Configurations.Products;

internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("Ratings");
        builder.ConfigureAuditing();

        builder.Property(t => t.ProductVariantId).HasColumnName(nameof(Rating.ProductVariantId)).IsRequired();
        builder.Property(t => t.RatingValue).HasColumnName(nameof(Rating.RatingValue)).IsRequired();
        builder.Property(t => t.Comment).HasColumnName(nameof(Rating.Comment)).HasMaxLength(RatingConsts.CommentMaxLength);
        builder.Property(t => t.Response).HasColumnName(nameof(Rating.Response)).HasMaxLength(RatingConsts.ResponseMaxLength);
        builder.Property(t => t.Status).HasColumnName(nameof(Rating.Status)).HasConversion<EnumToStringConverter<RatingStatus>>().IsRequired();
        
        builder.HasOne(t => t.ProductVariant).WithMany(t => t.Ratings).HasForeignKey(t => t.ProductVariantId);
        builder.HasOne(t => t.Customer).WithMany().HasForeignKey(t => t.CustomerId);
    }
}
