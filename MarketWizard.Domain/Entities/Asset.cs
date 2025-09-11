using System.ComponentModel.DataAnnotations.Schema;
using MarketWizard.Domain.Entities.Common;

namespace MarketWizard.Domain.Entities;

public enum AssetType
{
    Stock,
    ETF,
    Crypto
}

public partial class Asset : BaseEntity
{
    public string Symbol { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public AssetType Type { get; set; }
    
    public ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
    
    public ICollection<AssetPriceHistory> PriceHistories { get; set; } = new List<AssetPriceHistory>();
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
