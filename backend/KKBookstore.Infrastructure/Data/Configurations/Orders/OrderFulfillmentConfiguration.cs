using KKBookstore.Data.Extensions;
using KKBookstore.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Orders;

internal class OrderFulfillmentConfiguration : IEntityTypeConfiguration<OrderFulfillment>
{
    public void Configure(EntityTypeBuilder<OrderFulfillment> builder)
    {
        builder.ToTable("OrderFulfillments");
        builder.ConfigureAuditing();

        builder.Property(of => of.OrderId).IsRequired();
        builder.Property(of => of.BranchId).IsRequired();
        builder.Property(of => of.Status).IsRequired().HasConversion<string>();
        builder.Property(of => of.DistanceFromCustomer).HasPrecision(10, 2).IsRequired();
        builder.Property(of => of.IsSelectedForPackaging).IsRequired();
        builder.Property(of => of.PackagingStartedWhen).IsRequired(false);
        builder.Property(of => of.PackagingCompletedWhen).IsRequired(false);
        builder.Property(of => of.Notes).HasMaxLength(1000).IsRequired(false);        // Relationships
        builder.HasOne(of => of.Order)
            .WithMany(o => o.OrderFulfillments)
            .HasForeignKey(of => of.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(of => of.Branch)
            .WithMany()
            .HasForeignKey(of => of.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(of => of.OrderLineAllocations)
            .WithOne(ola => ola.OrderFulfillment)
            .HasForeignKey(ola => ola.OrderFulfillmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(of => of.OrderId);
        builder.HasIndex(of => of.BranchId);
        builder.HasIndex(of => new { of.OrderId, of.BranchId }).IsUnique();
    }
}
