using MarketWizard.Domain;

namespace MarketWizard.Data;

public class Inventory
{
    public Inventory()
    {
        Assets = CreateAssets();
        Portfolios = CreatePortfolios(Assets);
    }
    
    public IEnumerable<Portfolio> Portfolios { get; set; }
    
    public IEnumerable<Asset> Assets { get; set; }

    private IEnumerable<Portfolio> CreatePortfolios(IEnumerable<Asset> assets)
    {
        yield return new Portfolio()
        {
            Id = Guid.NewGuid(),
            Name = "Portfolio 1",
            Description = "Description 1",
            ImageUrl = "image",
            PortfolioAssets = new List<PortfolioAsset>().AsQueryable(),
        };
    }

    private IEnumerable<Asset> CreateAssets()
    {
        yield return new Asset()
        {
            Id = Guid.NewGuid(),
            Name = "Asset 1",
            Description = "Description 1",
            Symbol = "ASML"
        };
        
        yield return new Asset()
        {
            Id = Guid.NewGuid(),
            Name = "Asset 2",
            Description = "Description 2",
            Symbol = "DECK"
        };
    }
}

public static class InventoryFactory
{
    public static Inventory Create()
    {
        return new Inventory();
    }
}