namespace MarketWizard.Domain;

public enum PortfolioOperationType
{
    Buy,
    Sell
}

public class PortfolioAsset
{
    public Guid Id { get; set; }
    
    public Asset Asset { get; set; }
    
    public PortfolioOperationType Type { get; set; }
    
    public double NumberOfShares { get; set; }
    
    public double PricePerShare { get; set; }
}