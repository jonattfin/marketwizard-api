using MarketWizard.Domain;
using MarketWizard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var inventory = InventoryFactory.Create();
        
        var users = inventory.Users.ToList();
        var assets = inventory.Assets.ToList();
        var assetsPricesHistory = inventory.AssetsPricesHistory.ToList();
        var portfolios = inventory.Portfolios.ToList();
        
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Asset>().HasData(assets);
        modelBuilder.Entity<AssetPriceHistory>().HasData(assetsPricesHistory);
        modelBuilder.Entity<Portfolio>().HasData(portfolios);
    }
    
}