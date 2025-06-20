using KKBookstore.Customers;
using KKBookstore.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Customers;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasBaseType<User>();

        builder.Property(x => x.CustomerTypeId).HasColumnName(nameof(Customer.CustomerTypeId)).IsRequired();

        builder.HasOne(x => x.CustomerType)
            .WithMany()
            .HasForeignKey(e => e.CustomerTypeId)
            .IsRequired().OnDelete(DeleteBehavior.Restrict);
    }
}