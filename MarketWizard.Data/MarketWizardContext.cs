using MarketWizard.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data;

public class MarketWizardContext : DbContext
{
    public DbSet<Asset> Assets { get; set; }
}