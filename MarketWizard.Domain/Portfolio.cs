namespace MarketWizard.Domain;

public class Portfolio : IEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string ImageUrl { get; set; }

    public IQueryable<PortfolioAsset> PortfolioAssets { get; set; }
    
    public IQueryable<PortfolioNews> PortfolioNews { get; set; }
}