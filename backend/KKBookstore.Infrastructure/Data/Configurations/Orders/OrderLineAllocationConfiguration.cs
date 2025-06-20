using KKBookstore.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Orders;

internal class OrderLineAllocationConfiguration : IEntityTypeConfiguration<OrderLineAllocation>
{
    public void Configure(EntityTypeBuilder<OrderLineAllocation> builder)
    {
        builder.ToTable("OrderLineAllocations");

        builder.HasKey(ola => ola.Id);

        builder.Property(ola => ola.OrderLineId).IsRequired();
        builder.Property(ola => ola.OrderFulfillmentId).IsRequired();
        builder.Property(ola => ola.ProductVariantId).IsRequired();
        builder.Property(ola => ola.Quantity).IsRequired();
        builder.Property(ola => ola.UnitPrice).HasPrecision(18, 2).IsRequired();
        builder.Property(ola => ola.InventoryId).IsRequired();

        // Relationships
        builder.HasOne(ola => ola.OrderLine)
            .WithMany()
            .HasForeignKey(ola => ola.OrderLineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ola => ola.OrderFulfillment)
            .WithMany(of => of.OrderLineAllocations)
            .HasForeignKey(ola => ola.OrderFulfillmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ola => ola.ProductVariant)
            .WithMany()
            .HasForeignKey(ola => ola.ProductVariantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ola => ola.Inventory)
            .WithMany()
            .HasForeignKey(ola => ola.InventoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(ola => ola.OrderLineId);
        builder.HasIndex(ola => ola.OrderFulfillmentId);
        builder.HasIndex(ola => ola.ProductVariantId);
        builder.HasIndex(ola => ola.InventoryId);
    }
}
