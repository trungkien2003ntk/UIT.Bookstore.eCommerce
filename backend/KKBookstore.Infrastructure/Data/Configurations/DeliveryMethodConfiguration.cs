using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    // config this like #AuthorConfiguration
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.ToTable($"{nameof(DeliveryMethod)}s");

        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(DeliveryMethod)}Id");

        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(500);


        builder.ConfigureAuditing();
    }
}
