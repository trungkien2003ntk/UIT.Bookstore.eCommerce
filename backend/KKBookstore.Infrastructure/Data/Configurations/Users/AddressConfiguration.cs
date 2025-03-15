using KKBookstore.Domain.Branches;
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
        builder.Property(t => t.ProvinceId).HasColumnName(nameof(Address.ProvinceId)).IsRequired();
        builder.Property(t => t.ProvinceName).HasColumnName(nameof(Address.ProvinceName)).HasMaxLength(AddressConsts.ProvinceNameMaxLength).IsRequired();
        builder.Property(t => t.DistrictId).HasColumnName(nameof(Address.DistrictId)).IsRequired();
        builder.Property(t => t.DistrictName).HasColumnName(nameof(Address.DistrictName)).HasMaxLength(AddressConsts.DistrictNameMaxLength).IsRequired();
        builder.Property(t => t.CommuneCode).HasColumnName(nameof(Address.CommuneCode)).IsRequired();
        builder.Property(t => t.CommuneName).HasColumnName(nameof(Address.CommuneName)).HasMaxLength(AddressConsts.CommuneNameMaxLength).IsRequired();
        builder.Property(t => t.DetailAddress).HasColumnName(nameof(Address.DetailAddress)).HasMaxLength(AddressConsts.DetailAddressMaxLength).IsRequired();
        builder.Property(t => t.IsDefault).HasColumnName(nameof(Address.IsDefault)).IsRequired();
        builder.Property(t => t.Type).HasColumnName(nameof(Address.Type)).HasConversion<EnumToStringConverter<AddressType>>();

        builder.HasDiscriminator<string>("Discriminator")
            .HasValue<Address>(nameof(Address))
            .HasValue<BranchAddress>(nameof(BranchAddress))
            .HasValue<ShippingAddress>(nameof(ShippingAddress));
    }
}