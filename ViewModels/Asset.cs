namespace MarketWizardApi.ViewModels;

public enum AssetType
{
    // Define your asset types here
    Stock,
    Bond,
    Crypto,
    Commodity,
    Index
}

public class Asset(Guid id, double price, string symbol, double chg, string description, AssetType assetType)
{
    public Guid Id { get; set; } = id;
    public double Price { get; set; } = price;
    public string Symbol { get; set; } = symbol;
    public double Chg { get; set; } = chg;
    public string Description { get; set; } = description;
    public AssetType AssetType { get; set; } = assetType;

    public Asset(): this(Guid.NewGuid(), 0, string.Empty, 0, string.Empty, AssetType.Stock)
    {
    }
}