using MarketWizard.Domain.Entities.Common;

namespace MarketWizard.Domain.Entities;

public enum PortfolioOperationType
{
    Buy,
    Sell
}

public class PortfolioAsset : BaseEntity
{
    public Asset Asset { get; set; }
    
    public Guid AssetId { get; set; }
    
    public PortfolioOperationType Type { get; set; }
    
    public double NumberOfShares { get; set; }
    
    public double PricePerShare { get; set; }
}