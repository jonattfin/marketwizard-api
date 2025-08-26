namespace MarketWizard.Domain;

public enum PortfolioOperationType
{
    Buy,
    Sell
}

public class PortfolioAsset : IEntity
{
    public Guid Id { get; set; }
    
    public Asset Asset { get; set; }
    
    public Guid AssetId { get; set; }
    
    public PortfolioOperationType Type { get; set; }
    
    public double NumberOfShares { get; set; }
    
    public double PricePerShare { get; set; }
}