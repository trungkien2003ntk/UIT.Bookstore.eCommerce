using KKBookstore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id)
            .HasColumnName($"{nameof(User)}Id");

        builder.Property(u => u.FirstName)
            .HasMaxLength(300)
            .IsRequired();
        
        builder.Property(u => u.LastName)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(u => u.LoginType)
            .HasConversion<string>();

        builder.Property(u => u.ImageUrl)
            .HasMaxLength(500);

        builder.Property(u => u.Status)
            .HasConversion<string>();

        builder.Property(u => u.IsDeleted)
            .IsRequired();

        builder.HasOne(u => u.LastEditedByUser)
            .WithMany()
            .HasForeignKey(u => u.LastEditedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
