using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Shared.Users;
using KKBookstore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(u => u.FirstName).HasColumnName(nameof(User.FirstName)).HasMaxLength(UserConsts.FirstNameMaxLength).IsRequired();
        builder.Property(u => u.LastName).HasColumnName(nameof(User.LastName)).HasMaxLength(UserConsts.LastNameMaxLength).IsRequired();
        builder.Property(u => u.ImageUrl).HasColumnName(nameof(User.ImageUrl));
        builder.Property(u => u.LoginType).HasConversion<string>();
        builder.Property(u => u.Status).HasConversion<string>();

        builder.HasDiscriminator<string>("Discriminator")
            .HasValue<User>(nameof(User))
            .HasValue<Customer>(nameof(Customer));
    }
}
