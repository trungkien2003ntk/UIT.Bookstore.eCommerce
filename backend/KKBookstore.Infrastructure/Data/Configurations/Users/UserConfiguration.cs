using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Shared.Users;
using KKBookstore.Domain.Staffs;
using KKBookstore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Infrastructure.Data.Configurations.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("AspNetUsers");

        builder.Property(u => u.FirstName).HasColumnName(nameof(User.FirstName)).HasMaxLength(UserConsts.FirstNameMaxLength).IsRequired();
        builder.Property(u => u.LastName).HasColumnName(nameof(User.LastName)).HasMaxLength(UserConsts.LastNameMaxLength).IsRequired();
        builder.Property(u => u.ImageUrl).HasColumnName(nameof(User.ImageUrl));
        builder.Property(u => u.LoginType).HasColumnName(nameof(User.LoginType));
        builder.Property(u => u.Status).HasColumnName(nameof(User.Status));
        builder.Property(u => u.Status).HasColumnName(nameof(User.Status));
        builder.Property(u => u.SignInSource).HasColumnName(nameof(User.SignInSource));

        builder.Property(u => u.SignInSource).HasConversion<EnumToStringConverter<SignInSource>>();
        builder.Property(u => u.LoginType).HasConversion<EnumToStringConverter<LoginType>>();
        builder.Property(u => u.Status).HasConversion<EnumToStringConverter<UserStatus>>();

        builder.HasDiscriminator(u => u.SignInSource)
            .HasValue<User>(SignInSource.Default)
            .HasValue<Customer>(SignInSource.CustomerPortal)
            .HasValue<Staff>(SignInSource.AdminPortal);
    }
}
