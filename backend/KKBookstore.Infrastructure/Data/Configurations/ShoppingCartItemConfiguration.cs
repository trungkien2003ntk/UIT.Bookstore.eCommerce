using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(ShoppingCartItem)}Id");

        builder.Property(t => t.SkuId)
            .IsRequired();

        builder.Property(t => t.Quantity)
            .IsRequired();

        builder.HasOne(t => t.Sku)
            .WithMany()
            .HasForeignKey(t => t.SkuId);

        builder.HasOne(t => t.Customer)
            .WithMany()
            .HasForeignKey(t => t.CustomerId);

        builder.ConfigureAuditing();
    }
}
