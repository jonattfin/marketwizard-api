namespace MarketWizardApi.ViewModels;

public class PortfolioRatio
{
    public double Beta { get; set; }
    public double Sharpe { get; set; }
    public double Sortino { get; set; }
}

public class PortfolioReturns
{
    public Guid AssetId { get; set; }
    
    public string AssetName { get; set; }
    
    public IEnumerable<double> Months { get; set; }
    
    public IEnumerable<double> Weeks { get; set; }
}

public class PortfolioPerformance
{
    public Guid Id { get; set; }
    
    public string PortfolioId { get; set; }
    
    public PortfolioRatio Ratio { get; set; }
    
    public IEnumerable<PortfolioReturns> Returns { get; set; } = [];
}