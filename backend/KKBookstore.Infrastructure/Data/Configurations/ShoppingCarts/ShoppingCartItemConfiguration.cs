using KKBookstore.Domain.ShoppingCarts;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.ShoppingCarts;

internal class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.ToTable("ShoppingCartItems");
        builder.ConfigureAuditing();

        builder.Property(t => t.ProductVariantId).HasColumnName(nameof(ShoppingCartItem.ProductVariantId)).IsRequired();
        builder.Property(t => t.Quantity).HasColumnName(nameof(ShoppingCartItem.Quantity)).IsRequired();

        builder.HasOne(t => t.ProductVariant).WithMany().HasForeignKey(t => t.ProductVariantId);
        builder.HasOne(t => t.Customer).WithMany().HasForeignKey(t => t.CustomerId);
    }
}
