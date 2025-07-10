using Domain.Aggregates.RefreshToken;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasIndex(x => x.Token)
            .IsUnique();

        builder.Property(x => x.Token)
            .HasMaxLength(256);

        builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId);
    }
}
