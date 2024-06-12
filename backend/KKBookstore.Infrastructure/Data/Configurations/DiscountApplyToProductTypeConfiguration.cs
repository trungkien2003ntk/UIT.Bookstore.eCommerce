using KKBookstore.Domain.Aggregates.OrderAggregate;
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
            .WithMany()
            .HasForeignKey(x => x.DiscountVoucherId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
