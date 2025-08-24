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
    
    public Guid AssetId { get; set; }
    
    public PortfolioOperationType Type { get; set; }
    
    public double NumberOfShares { get; set; }
    
    public double PricePerShare { get; set; }
}

public class PortfolioNews
{
    public Guid Id { get; set; }
    
    public Asset Asset { get; set; }
    
    public Guid AssetId { get; set; }
    
    public string Time { get; set; }
    
    public string Symbol { get; set; }
    
    public string Headline { get; set; }
    
    public string Provider {get; set; }
}

public class PortfolioRatio
{
    public double BetaRatio { get; set; }
    
    public double SharpeRatio { get; set; }
    
    public double SortinoRatio { get; set; }
}

public class AssetReturn
{
    public string AssetName { get; set; }
    
    public double[] WeeklyReturns { get; set; }
    
    public double[] MonthlyReturns { get; set; }
}

public class PortfolioPerformance
{
    public Guid Id { get; set; }

    public PortfolioRatio Ratio { get; set; }
    
    public AssetReturn[] Returns { get; set; }
}