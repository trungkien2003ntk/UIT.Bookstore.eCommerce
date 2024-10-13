using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ShippingAddressConfiguration : IEntityTypeConfiguration<ShippingAddress>
{
    public void Configure(EntityTypeBuilder<ShippingAddress> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(ShippingAddress)}Id");

        builder.Property(t => t.UserId)
            .IsRequired();

        builder.Property(t => t.Province)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.District)
            .HasMaxLength(100);

        builder.Property(t => t.Commune)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.DetailAddress)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(t => t.IsDefault)
            .IsRequired();

        builder.Property(t => t.AddressTypeId)
            .IsRequired();

        builder.Ignore(t => t.AddressTypeEnum);

        builder.HasOne(t => t.AddressType)
            .WithMany()
            .HasForeignKey(t => t.AddressTypeId);


        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);

        builder.ConfigureAuditing();
    }
}
