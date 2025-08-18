namespace MarketWizard.Domain;

public class Portfolio
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }

    public ICollection<PortfolioAsset> Assets { get; set; } = [];
}