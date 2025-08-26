using MarketWizard.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // var inventory = InventoryFactory.Create();
        //
        // modelBuilder.Entity<Portfolio>().HasData(inventory.Portfolios.ToList());
        // modelBuilder.Entity<Asset>().HasData(inventory.Assets.ToList());
    }
    
}