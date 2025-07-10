using Domain.Aggregates.RefreshToken;
using Domain.Common;
using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Persistence.Contexts;

internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

        foreach (EntityEntry entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                SetProperty(entry, nameof(IAuditableEntity.CreatedOnUtc));
            }

            SetProperty(entry, nameof(IAuditableEntity.UpdatedOnUtc));
        }

        return base.SaveChangesAsync(cancellationToken);

        void SetProperty(EntityEntry entityEntry, string propertyName)
        {
            try
            {
                PropertyEntry propertyEntry = entityEntry.Property(propertyName);
                propertyEntry.CurrentValue = DateTime.UtcNow;
            }
            catch { }
        }
    }

    public override int SaveChanges() => 
        SaveChangesAsync().GetAwaiter().GetResult();
}
