using KKBookstore.Data.Extensions;
using KKBookstore.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Data.Configurations.Orders;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.ConfigureAuditing();

        builder.Property(o => o.OrderNumber).HasColumnName(nameof(Order.OrderNumber)).HasMaxLength(OrderConsts.OrderNumberMaxLength).IsRequired();
        builder.Property(o => o.Comment).HasColumnName(nameof(Order.Comment)).HasMaxLength(OrderConsts.CommentMaxLength);
        builder.Property(o => o.DeliveryInstruction).HasColumnName(nameof(Order.DeliveryInstruction)).HasMaxLength(OrderConsts.DeliveryInstructionMaxLength);        builder.HasOne(o => o.ShippingAddress).WithMany().HasForeignKey(o => o.ShippingAddressId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(o => o.PaymentMethod).WithMany().HasForeignKey(o => o.PaymentMethodId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(o => o.DeliveryMethod).WithMany().HasForeignKey(o => o.DeliveryMethodId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(o => o.PriceDiscountVoucher).WithMany().HasForeignKey(o => o.PriceDiscountVoucherId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(o => o.ShippingDiscountVoucher).WithMany().HasForeignKey(o => o.ShippingDiscountVoucherId).OnDelete(DeleteBehavior.NoAction);        // Configure navigation properties (collections)
        builder.HasMany(o => o.OrderLines).WithOne(ol => ol.Order).HasForeignKey(ol => ol.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(o => o.Transactions).WithOne(t => t.Order).HasForeignKey(t => t.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(o => o.OrderFulfillments).WithOne(of => of.Order).HasForeignKey(of => of.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(o => o.OrderHistories).WithOne(oh => oh.Order).HasForeignKey(oh => oh.OrderId).OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.TaxRate).HasPrecision(18, 2);
        builder.Property(o => o.ShippingFee).HasPrecision(18, 2);
        builder.Property(o => o.Status).IsRequired().HasConversion<EnumToStringConverter<OrderStatus>>();
    }
}
