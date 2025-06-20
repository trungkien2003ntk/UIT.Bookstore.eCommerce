using KKBookstore.Branches;
using KKBookstore.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Branches;

public class BranchAddressConfiguration : IEntityTypeConfiguration<BranchAddress>
{
    public void Configure(EntityTypeBuilder<BranchAddress> builder)
    {
        builder.HasBaseType<Address>();
        builder.Property(t => t.BranchId).HasColumnName(nameof(BranchAddress.BranchId)).IsRequired();

        builder.HasOne<Branch>().WithOne(b => b.Address).HasForeignKey<BranchAddress>(b => b.BranchId).OnDelete(DeleteBehavior.Cascade);
    }
}