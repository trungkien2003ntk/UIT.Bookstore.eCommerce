﻿using KKBookstore.Domain.Orders;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Orders;

internal class DiscountApplyToProductTypeConfiguration : IEntityTypeConfiguration<DiscountApplyToProductType>
{
    public void Configure(EntityTypeBuilder<DiscountApplyToProductType> builder)
    {
        builder.ToTable("DiscountApplyToProductTypes");
        builder.ConfigureAuditing();

        builder.HasOne(x => x.ProductType)
            .WithMany()
            .HasForeignKey(x => x.ProductTypeId);

        builder.HasOne(x => x.DiscountVoucher)
            .WithMany(dv => dv.ProductTypesApplied)
            .HasForeignKey(x => x.DiscountVoucherId);
    }
}
