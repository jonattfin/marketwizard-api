namespace MarketWizardApi.ViewModels;

public class Portfolio
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime LastUpdated { get; set; }
    public double TotalAmount { get; set; }
    public RiskLevel Risk { get; set; }
    public double AverageAnnualReturn { get; set; }
    public double StandardDeviation { get; set; }
    public double SharpeRatio { get; set; }
    public double MaximumDrawdown { get; set; }

    public Portfolio()
    {
        
    }
}

public enum RiskLevel
{
    Low,
    Medium,
    High
}