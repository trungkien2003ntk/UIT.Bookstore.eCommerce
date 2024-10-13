using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        var converter = new EnumToStringConverter<OrderStatus>();

        builder.ToTable($"{nameof(Order)}s");

        builder.HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId);

        builder.Property(o => o.Id)
            .HasColumnName("OrderId");

        builder.HasOne(o => o.ShippingAddress)
            .WithMany()
            .HasForeignKey(o => o.ShippingAddressId);

        builder.HasOne(o => o.PaymentMethod)
            .WithMany()
            .HasForeignKey(o => o.PaymentMethodId);

        builder.HasOne(o => o.DeliveryMethod)
            .WithMany()
            .HasForeignKey(o => o.DeliveryMethodId);

        builder.HasOne(o => o.PriceDiscountVoucher)
            .WithMany()
            .HasForeignKey(o => o.PriceDiscountVoucherId);

        builder.HasOne(o => o.ShippingDiscountVoucher)
            .WithMany()
            .HasForeignKey(o => o.ShippingDiscountVoucherId);

        builder.HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId);

        builder.Property(o => o.TaxRate)
            .HasPrecision(18, 2);

        builder.Property(o => o.ShippingFee)
            .HasPrecision(18, 2);

        builder.Property(o => o.Status)
            .IsRequired()
            .HasConversion(converter);

        builder.ConfigureAuditing();
    }
}
