using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class DiscountApplyToProductTypeConfiguration : IEntityTypeConfiguration<DiscountApplyToProductType>
{
    public void Configure(EntityTypeBuilder<DiscountApplyToProductType> builder)
    {
        builder.ToTable($"{nameof(DiscountApplyToProductType)}s");

        builder.HasKey(x => x.Id)
            .HasName($"{nameof(DiscountApplyToProductType)}Id");

        builder.HasOne(x => x.ProductType)
            .WithMany()
            .HasForeignKey(x => x.ProductTypeId);

        builder.HasOne(x => x.DiscountVoucher)
            .WithMany(dv => dv.ProductTypesApplied)
            .HasForeignKey(x => x.DiscountVoucherId);


        builder.ConfigureAuditing();
    }
}
