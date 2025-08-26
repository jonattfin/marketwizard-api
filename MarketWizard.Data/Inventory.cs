using MarketWizard.Domain;

namespace MarketWizard.Data;

public class Inventory
{
    public Inventory()
    {
        Users = CreateUsers();
        Assets = CreateAssets().ToList();
        AssetsPricesHistory = CreateAssetsPricesHistory(Assets);
        Portfolios = CreatePortfolios(Users, Assets);
    }
    
    public IEnumerable<User> Users { get; set; }
    
    public IEnumerable<Portfolio> Portfolios { get; set; }
    
    public IEnumerable<Asset> Assets { get; set; }
    
    public IEnumerable<AssetPriceHistory> AssetsPricesHistory { get; set; }

    private IEnumerable<User> CreateUsers()
    {
        yield return new User()
        {
            Id = Guid.NewGuid(),
            Name = "John",
            Email = "john.doe@marketwizard.com"
        };

        yield return new User()
        {
            Id = Guid.NewGuid(),
            Name = "Jane",
            Email = "jane.doe@marketwizard.com"
        };
    }
    
    private IEnumerable<Portfolio> CreatePortfolios(IEnumerable<User> users, IEnumerable<Asset> assets)
    {
        var portfoliosNames = new List<string>() {"Portfolio 1", "Portfolio 2", "Portfolio 3"};
        
        foreach (var user in users)
        {
            foreach (var portfolioName in portfoliosNames)
            {
                yield return CreatePortfolio(portfolioName, user);
            }
        }

        Portfolio CreatePortfolio(string portfolioName, User user)
        {
            return new Portfolio()
            {
                Id = Guid.NewGuid(),
                Name = portfolioName,
                Description = "Description",
                UserId = user.Id,
                ImageUrl = "https://images.unsplash.com/photo-1611974789855-9c2a0a7236a3",
                PortfolioAssets = assets.Select(asset => new PortfolioAsset()
                {
                    Id = Guid.NewGuid(),
                    Type = PortfolioOperationType.Buy,
                    Asset = asset,
                    AssetId = asset.Id,
                    NumberOfShares = 10,
                    PricePerShare = 0,
                }),
            };
        }
        
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
        yield return CreateAsset("BTC", AssetType.Crypto);
        yield return CreateAsset("VWRL", AssetType.ETF);
        yield return CreateAsset("ASML", AssetType.Stock);
        
        yield break;

        Asset CreateAsset(string assetSymbol, AssetType assetType)
        {
            var asset = new Asset()
            {
                Id = Guid.NewGuid(),
                Name = assetSymbol,
                Description = assetSymbol,
                Symbol = assetSymbol,
                Type = assetType,
                PriceHistories = Enumerable.Empty<AssetPriceHistory>().AsQueryable()
            };
            
            return asset;
        }
    }
    
    IEnumerable<AssetPriceHistory> CreateAssetsPricesHistory(IEnumerable<Asset> assets)
        {
            var random = new Random();

            foreach (var asset in assets)
            {
                 foreach (var index in Enumerable.Range(1, 365))
                 {
                     yield return new AssetPriceHistory()
                     {
                         Id = Guid.NewGuid(),
                         AssetId = asset.Id,
                         Price = random.Next(100, 300),
                         Date = DateTime.UtcNow.AddDays(-index)
                     };
                 }
            }
        }
}

public static class InventoryFactory
{
    public static Inventory Create()
    {
        return new Inventory();
    }
}