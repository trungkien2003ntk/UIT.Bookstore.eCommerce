using KKBookstore.Domain.Products;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Products;

internal class RatingLikeConfiguration : IEntityTypeConfiguration<RatingLike>
{
    public void Configure(EntityTypeBuilder<RatingLike> builder)
    {
        builder.ToTable("RatingLikes");
        builder.ConfigureAuditing();

        builder.Property(t => t.CustomerId).HasColumnName(nameof(RatingLike.CustomerId)).IsRequired();
        builder.Property(t => t.RatingId).HasColumnName(nameof(RatingLike.RatingId)).IsRequired();
        builder.Property(t => t.Liked).HasColumnName(nameof(RatingLike.Liked)).IsRequired();
        
        builder.HasOne(t => t.Customer).WithMany().HasForeignKey(t => t.CustomerId);
        builder.HasOne(t => t.Rating).WithMany(r => r.Likes).HasForeignKey(t => t.RatingId);
    }
}
