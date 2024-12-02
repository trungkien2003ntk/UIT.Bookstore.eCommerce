using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Shared.Orders;
using KKBookstore.Domain.Shared.Users;
using KKBookstore.Domain.Users;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Infrastructure.Data.Configurations.Users;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
        builder.ConfigureAuditing();

        builder.Property(t => t.PhoneNumber).HasColumnName(nameof(Address.PhoneNumber)).HasMaxLength(AddressConsts.PhoneNumberMaxLength).IsRequired();
        builder.Property(t => t.Province).HasColumnName(nameof(Address.Province)).HasMaxLength(AddressConsts.ProvinceMaxLength).IsRequired();
        builder.Property(t => t.District).HasColumnName(nameof(Address.District)).HasMaxLength(AddressConsts.DistrictMaxLength).IsRequired();
        builder.Property(t => t.Commune).HasColumnName(nameof(Address.Commune)).HasMaxLength(AddressConsts.CommuneMaxLength).IsRequired();
        builder.Property(t => t.DetailAddress).HasColumnName(nameof(Address.DetailAddress)).HasMaxLength(AddressConsts.DetailAddressMaxLength).IsRequired();
        builder.Property(t => t.IsDefault).HasColumnName(nameof(Address.IsDefault)).IsRequired();
        builder.Property(t => t.Type).HasColumnName(nameof(Address.Type)).HasConversion<EnumToStringConverter<AddressType>>();

        builder.HasDiscriminator<string>("Discriminator")
            .HasValue<Address>(nameof(Address))
            .HasValue<ShippingAddress>(nameof(ShippingAddress));
    }
}