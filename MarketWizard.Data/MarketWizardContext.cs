using MarketWizard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data;

public class MarketWizardContext(DbContextOptions<MarketWizardContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Asset> Assets { get; set; }
    
    public DbSet<Portfolio> Portfolios { get; set; }
    
}