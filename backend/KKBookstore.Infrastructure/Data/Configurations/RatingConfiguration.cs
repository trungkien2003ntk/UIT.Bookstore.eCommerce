using KKBookstore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(Rating)}Id");

        builder.Property(t => t.SkuId)
            .IsRequired();

        builder.Property(t => t.RatingValue)
            .IsRequired();

        builder.Property(t => t.Comment)
            .HasMaxLength(500);

        builder.HasOne(t => t.Sku)
            .WithMany(t => t.Ratings)
            .HasForeignKey(t => t.SkuId);

        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
