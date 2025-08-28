using System.ComponentModel.DataAnnotations.Schema;
using MarketWizard.Domain.Entities.Interfaces;

namespace MarketWizard.Domain.Entities;

public enum AssetType
{
    Stock,
    ETF,
    Crypto
}

public partial class Asset : IEntity
{
    public Guid Id { get; set; }
    
    public string Symbol { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public AssetType Type { get; set; }
    
    public IEnumerable<AssetPriceHistory> PriceHistories { get; set; } = new List<AssetPriceHistory>();
}

public partial class Asset
{
    [NotMapped]
    public double? LastPrice
    {
        get
        {
            return PriceHistories.OrderByDescending(x => x.Date).FirstOrDefault()?.Price;
        }
    }
}
