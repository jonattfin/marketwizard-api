namespace MarketWizard.Domain;

public enum AssetType
{
    Stock,
    ETF,
    Crypto
}

public class Asset : IEntity
{
    public Guid Id { get; set; }
    
    public string Symbol { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public AssetType Type { get; set; }
    
    public IEnumerable<AssetPriceHistory> PriceHistories { get; set; } = new List<AssetPriceHistory>();
}
