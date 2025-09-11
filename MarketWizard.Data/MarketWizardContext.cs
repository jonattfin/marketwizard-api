using MarketWizard.Domain.Entities;
using MarketWizard.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data;

public class MarketWizardContext(DbContextOptions<MarketWizardContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Portfolio> Portfolios { get; set; }

    public DbSet<Watchlist> Watchlists { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}