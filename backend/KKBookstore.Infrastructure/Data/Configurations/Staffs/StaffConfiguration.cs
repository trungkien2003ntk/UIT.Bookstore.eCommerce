using KKBookstore.Domain.Shared.Staffs;
using KKBookstore.Domain.Staffs;
using KKBookstore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Staffs;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.HasBaseType<User>();

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(StaffConsts.DescriptionMaxLength);
    }
}