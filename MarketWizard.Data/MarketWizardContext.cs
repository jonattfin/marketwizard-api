using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Domain.Entities;
using MarketWizard.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data;

public class MarketWizardContext(DbContextOptions<MarketWizardContext> options, IUserService userService)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Portfolio> Portfolios { get; set; }

    public DbSet<Watchlist> Watchlists { get; set; }

    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var authenticatedUserId = userService.GetAuthenticatedUserId();

        modelBuilder.Entity<Portfolio>().HasQueryFilter(p => p.UserId == authenticatedUserId);
        modelBuilder.Entity<Watchlist>().HasQueryFilter(p => p.UserId == authenticatedUserId);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var authenticatedUserId = userService.GetAuthenticatedUserId();

        foreach (var entry in base.ChangeTracker.Entries()
                     .Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            if (entry.Entity is BaseEntity entity)
            {
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = authenticatedUserId;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = authenticatedUserId;
                }
            }

            if (entry.Entity is IUserScopedEntity userScopedEntity)
            {
                userScopedEntity.UserId = authenticatedUserId;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}