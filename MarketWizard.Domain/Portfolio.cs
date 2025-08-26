namespace MarketWizard.Domain;

public class Portfolio : IEntity
{
    public Guid Id { get; set; }
    
    public User User { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string ImageUrl { get; set; }

    public IEnumerable<PortfolioAsset> PortfolioAssets { get; set; } = new List<PortfolioAsset>();
    
}