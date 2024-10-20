using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable($"{nameof(OrderLine)}s");

        builder.Property(t => t.Id)
            .HasColumnName("OrderLineId");

        builder.Property(t => t.OrderId)
            .IsRequired();

        builder.Property(t => t.ProductVariantId)
            .IsRequired();

        builder.Property(t => t.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.Quantity)
            .IsRequired();

        builder.HasOne(t => t.Order)
            .WithMany(t => t.OrderLines)
            .HasForeignKey(t => t.OrderId);

        builder.HasOne(t => t.ProductVariant)
            .WithMany()
            .HasForeignKey(t => t.ProductVariantId);


        builder.ConfigureAuditing();
    }
}
