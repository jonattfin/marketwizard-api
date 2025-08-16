namespace MarketWizardApi.ViewModels;

public class PortfolioAsset
{
    public string Id { get; set; }
    
    public string Symbol { get; set; }

    public string Description { get; set; }
    
    public double NumberOfShares { get; set; }
    
    public double PricePerShare { get; set; }
}