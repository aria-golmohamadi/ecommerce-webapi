using Identity.Constants;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(IdentityTableNames.Users);

        builder.Property(x => x.FirstName)
            .IsRequired(false)
            .HasMaxLength(256);

        builder.Property(x => x.LastName)
            .IsRequired(false)
            .HasMaxLength(256);
    }
}
