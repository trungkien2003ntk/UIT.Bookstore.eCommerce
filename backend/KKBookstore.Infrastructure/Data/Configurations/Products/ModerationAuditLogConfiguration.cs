using KKBookstore.Data.Extensions;
using KKBookstore.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Products;

internal class ModerationAuditLogConfiguration : IEntityTypeConfiguration<ModerationAuditLog>
{
    public void Configure(EntityTypeBuilder<ModerationAuditLog> builder)
    {
        builder.ToTable("ModerationAuditLogs");
        builder.ConfigureAuditing();

        builder.Property(t => t.RatingId).HasColumnName(nameof(ModerationAuditLog.RatingId)).IsRequired();
        builder.Property(t => t.Action).HasColumnName(nameof(ModerationAuditLog.Action)).IsRequired();
        builder.Property(t => t.Details).HasColumnName(nameof(ModerationAuditLog.Details)).IsRequired();
        builder.Property(t => t.ModeratorId).HasColumnName(nameof(ModerationAuditLog.ModeratorId));
        builder.Property(t => t.AiScore).HasColumnName(nameof(ModerationAuditLog.AiScore));
        builder.Property(t => t.Timestamp).HasColumnName(nameof(ModerationAuditLog.Timestamp)).IsRequired();

        // Enhanced moderation properties
        builder.Property(t => t.ModerationLevel).HasColumnName(nameof(ModerationAuditLog.ModerationLevel));
        builder.Property(t => t.ThresholdUsed).HasColumnName(nameof(ModerationAuditLog.ThresholdUsed));

        // Relationships
        builder.HasOne(t => t.Rating).WithMany().HasForeignKey(t => t.RatingId).OnDelete(DeleteBehavior.Cascade);
    }
}