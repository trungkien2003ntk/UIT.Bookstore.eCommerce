using KKBookstore.Staffs;
using KKBookstore.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Staffs;

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