using MarketWizard.Data.Extensions;
using MarketWizard.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data;

public class MarketWizardContext: DbContext
{
    public DbSet<Asset> Assets { get; set; }
    
    public DbSet<Portfolio> Portfolios { get; set; }
    
    public MarketWizardContext(DbContextOptions<MarketWizardContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}