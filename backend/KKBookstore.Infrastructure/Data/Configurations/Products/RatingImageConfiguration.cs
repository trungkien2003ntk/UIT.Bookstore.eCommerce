using KKBookstore.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Products;

public class RatingImageConfiguration : IEntityTypeConfiguration<RatingImage>
{
    public void Configure(EntityTypeBuilder<RatingImage> builder)
    {
        builder.ToTable("RatingImages");

        builder.Property(t => t.ImageUrl).HasColumnName(nameof(RatingImage.ImageUrl)).HasMaxLength(RatingImageConsts.ImageUrlMaxLength).IsRequired();
        builder.Property(t => t.RatingId).HasColumnName(nameof(RatingImage.RatingId)).IsRequired();

        builder.HasOne(t => t.Rating).WithMany(t => t.Images).HasForeignKey(t => t.RatingId).OnDelete(DeleteBehavior.Cascade);
    }
}
