using KKBookstore.Domain.Orders;
using KKBookstore.Domain.Shared.Orders;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Infrastructure.Data.Configurations.Orders;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.ConfigureAuditing();

        builder.Property(o => o.OrderNumber).HasColumnName(nameof(Order.OrderNumber)).HasMaxLength(OrderConsts.OrderNumberMaxLength).IsRequired();
        builder.Property(o => o.Comment).HasColumnName(nameof(Order.Comment)).HasMaxLength(OrderConsts.CommentMaxLength);
        builder.Property(o => o.DeliveryInstruction).HasColumnName(nameof(Order.DeliveryInstruction)).HasMaxLength(OrderConsts.DeliveryInstructionMaxLength);

        builder.HasOne(o => o.ShippingAddress).WithMany().HasForeignKey(o => o.ShippingAddressId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(o => o.PaymentMethod).WithMany().HasForeignKey(o => o.PaymentMethodId);
        builder.HasOne(o => o.DeliveryMethod).WithMany().HasForeignKey(o => o.DeliveryMethodId);
        builder.HasOne(o => o.PriceDiscountVoucher).WithMany().HasForeignKey(o => o.PriceDiscountVoucherId);
        builder.HasOne(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId);
        builder.HasOne(o => o.ShippingDiscountVoucher).WithMany().HasForeignKey(o => o.ShippingDiscountVoucherId);

        builder.Property(o => o.TaxRate).HasPrecision(18, 2);
        builder.Property(o => o.ShippingFee).HasPrecision(18, 2);
        builder.Property(o => o.Status).IsRequired().HasConversion<EnumToStringConverter<OrderStatus>>();
    }
}
