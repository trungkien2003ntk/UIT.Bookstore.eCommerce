using KKBookstore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName("OptionId");

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(t => t.Product)
            .WithMany()
            .HasForeignKey(t => t.ProductId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
