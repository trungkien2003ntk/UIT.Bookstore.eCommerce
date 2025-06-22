using KKBookstore.Orders;
using KKBookstore.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Orders;

internal class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        builder.ToTable("OrderHistories");
        
        builder.ConfigureAuditing();

        // Primary key
        builder.HasKey(oh => oh.Id);

        // Properties
        builder.Property(oh => oh.OrderId)
            .IsRequired();

        builder.Property(oh => oh.FromStatus)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(oh => oh.ToStatus)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(oh => oh.Action)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(oh => oh.Notes)
            .HasMaxLength(1000);

        builder.Property(oh => oh.TriggeredByUserId);

        builder.Property(oh => oh.Timestamp)
            .IsRequired();

        builder.Property(oh => oh.ExternalReference)
            .HasMaxLength(100);

        // Relationships
        builder.HasOne(oh => oh.Order)
            .WithMany(o => o.OrderHistories)
            .HasForeignKey(oh => oh.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oh => oh.TriggeredByUser)
            .WithMany()
            .HasForeignKey(oh => oh.TriggeredByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Indexes
        builder.HasIndex(oh => oh.OrderId);
        builder.HasIndex(oh => oh.FromStatus);
        builder.HasIndex(oh => oh.ToStatus);
        builder.HasIndex(oh => oh.Timestamp);
        builder.HasIndex(oh => new { oh.OrderId, oh.Timestamp });
    }
}
