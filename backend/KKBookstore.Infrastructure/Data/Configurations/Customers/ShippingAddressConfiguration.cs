using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Shared.Customers;
using KKBookstore.Domain.Users;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Customers;

internal class ShippingAddressConfiguration : IEntityTypeConfiguration<ShippingAddress>
{
    public void Configure(EntityTypeBuilder<ShippingAddress> builder)
    {
        builder.ToTable("ShippingAddresses");
        builder.HasBaseType<Address>();
        builder.Property(t => t.CustomerId).HasColumnName(nameof(ShippingAddress.CustomerId)).IsRequired();
        builder.Property(t => t.ReceiverName).HasColumnName(nameof(ShippingAddress.ReceiverName)).HasMaxLength(ShippingAddressConsts.ReceiverNameMaxLength).IsRequired();

        builder.HasOne(t => t.Customer)
            .WithMany(c => c.ShippingAddresses)
            .HasForeignKey(t => t.CustomerId);
    }
}
