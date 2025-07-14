using KKBookstore.Data.Extensions;
using KKBookstore.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Data.Configurations.Products;

internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("Ratings");
        builder.ConfigureAuditing();

        builder.Property(t => t.ProductVariantId).HasColumnName(nameof(Rating.ProductVariantId)).IsRequired();
        builder.Property(t => t.ProductId).HasColumnName(nameof(Rating.ProductId)).IsRequired();
        builder.Property(t => t.RatingValue).HasColumnName(nameof(Rating.RatingValue)).IsRequired();
        builder.Property(t => t.Comment).HasColumnName(nameof(Rating.Comment)).HasMaxLength(RatingConsts.CommentMaxLength);
        builder.Property(t => t.Response).HasColumnName(nameof(Rating.Response)).HasMaxLength(RatingConsts.ResponseMaxLength);
        builder.Property(t => t.Status).HasColumnName(nameof(Rating.Status)).HasConversion<EnumToStringConverter<RatingStatus>>().IsRequired();

        // AI Moderation properties
        builder.Property(t => t.AiModerationScore).HasColumnName(nameof(Rating.AiModerationScore));
        builder.Property(t => t.AiModerationCategory).HasColumnName(nameof(Rating.AiModerationCategory));
        builder.Property(t => t.AiModerationExplanation).HasColumnName(nameof(Rating.AiModerationExplanation));
        builder.Property(t => t.AiModerationDate).HasColumnName(nameof(Rating.AiModerationDate));
        builder.Property(t => t.IsAiModerated).HasColumnName(nameof(Rating.IsAiModerated)).IsRequired();
        builder.Property(t => t.ReportsCount).HasColumnName(nameof(Rating.ReportsCount)).IsRequired();

        // Enhanced moderation properties
        builder.Property(t => t.ModerationLevel).HasColumnName(nameof(Rating.ModerationLevel));

        // Sentiment analysis properties
        builder.Property(t => t.SentimentScore).HasColumnName(nameof(Rating.SentimentScore)).HasColumnType("decimal(5,4)");
        builder.Property(t => t.SentimentLabel).HasColumnName(nameof(Rating.SentimentLabel)).HasMaxLength(50);

        builder.HasOne(t => t.ProductVariant).WithMany(t => t.Ratings).HasForeignKey(t => t.ProductVariantId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Customer).WithMany().HasForeignKey(t => t.CustomerId).OnDelete(DeleteBehavior.SetNull);
    }
}
