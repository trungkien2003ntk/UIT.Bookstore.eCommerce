using KKBookstore.Domain.Aggregates.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class RatingLikeConfiguration : IEntityTypeConfiguration<RatingLike>
{
    public void Configure(EntityTypeBuilder<RatingLike> builder)
    {
        builder.ToTable($"{nameof(RatingLike)}s");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(RatingLike)}Id");

        builder.Property(t => t.UserId)
            .IsRequired();

        builder.Property(t => t.RatingId)
            .IsRequired();

        builder.Property(t => t.Liked)
            .IsRequired();

        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);

        builder.HasOne(t => t.Rating)
            .WithMany(r => r.Likes)
            .HasForeignKey(t => t.RatingId);
    }
}
