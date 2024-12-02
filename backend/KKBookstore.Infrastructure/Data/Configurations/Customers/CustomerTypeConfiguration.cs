using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Shared.Customers;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Customers;

internal class CustomerTypeConfiguration : IEntityTypeConfiguration<CustomerType>
{
    public void Configure(EntityTypeBuilder<CustomerType> builder)
    {
        builder.ToTable("CustomerTypes");
        builder.ConfigureAuditing();

        builder.Property(x => x.Name).HasColumnName(nameof(CustomerType.Name)).HasMaxLength(CustomerTypeConsts.NameMaxLength).IsRequired();
        builder.Property(x => x.MinSpending).HasColumnName(nameof(CustomerType.MinSpending)).HasPrecision(18,2).IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}