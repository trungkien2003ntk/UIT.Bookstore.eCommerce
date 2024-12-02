using KKBookstore.Domain.Orders;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Orders;

internal class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable("OrderLines");
        builder.ConfigureAuditing();

        builder.Property(t => t.OrderId).HasColumnName(nameof(OrderLine.OrderId)).IsRequired();
        builder.Property(t => t.ProductVariantId).HasColumnName(nameof(OrderLine.ProductVariantId)).IsRequired();
        builder.Property(t => t.UnitPrice).HasColumnName(nameof(OrderLine.UnitPrice)).HasPrecision(18, 2).IsRequired();
        builder.Property(t => t.Quantity).HasColumnName(nameof(OrderLine.Quantity)).IsRequired();
        builder.HasOne(t => t.Order).WithMany(t => t.OrderLines).HasForeignKey(t => t.OrderId);
        builder.HasOne(t => t.ProductVariant).WithMany().HasForeignKey(t => t.ProductVariantId);
    }
}
