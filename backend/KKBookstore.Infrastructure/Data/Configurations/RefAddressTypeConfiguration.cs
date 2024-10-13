﻿using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class RefAddressTypeConfiguration : IEntityTypeConfiguration<RefAddressType>
{
    public void Configure(EntityTypeBuilder<RefAddressType> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(RefAddressType)}Id");

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.IsDeleted)
            .IsRequired();

        builder.HasIndex(t => t.Name)
            .IsUnique();


        builder.ConfigureAuditing();
    }
}
