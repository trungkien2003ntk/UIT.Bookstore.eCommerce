using KKBookstore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Identity;

internal class BlacklistedTokenConfiguration : IEntityTypeConfiguration<BlacklistedToken>
{
    public void Configure(EntityTypeBuilder<BlacklistedToken> builder)
    {
        builder.ToTable("BlacklistedTokens");

        builder.HasKey(bt => bt.Id);

        builder.Property(bt => bt.Token)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(bt => bt.ExpireAt)
            .IsRequired();

        builder.Property(bt => bt.CreatedAt)
            .IsRequired();

        // Add index on Token for fast lookups
        builder.HasIndex(bt => bt.Token)
            .IsUnique();

        // Add index on ExpireAt for cleanup operations
        builder.HasIndex(bt => bt.ExpireAt);
    }
}
