using KKBookstore.Data.Extensions;
using KKBookstore.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Orders;

internal class VoucherCustomerTypeConfiguration : IEntityTypeConfiguration<VoucherCustomerType>
{
    public void Configure(EntityTypeBuilder<VoucherCustomerType> builder)
    {
        builder.ToTable("VoucherCustomerTypes");
        builder.ConfigureAuditing();

        // Composite primary key
        builder.HasKey(vct => new { vct.VoucherId, vct.CustomerTypeId });

        // Foreign key relationships
        builder.HasOne(vct => vct.Voucher)
            .WithMany(v => v.CustomerTypes)
            .HasForeignKey(vct => vct.VoucherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(vct => vct.CustomerType)
            .WithMany()
            .HasForeignKey(vct => vct.CustomerTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index for performance
        builder.HasIndex(vct => vct.VoucherId);
        builder.HasIndex(vct => vct.CustomerTypeId);
    }
}
