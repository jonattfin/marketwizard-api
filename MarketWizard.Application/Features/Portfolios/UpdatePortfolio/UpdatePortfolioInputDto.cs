namespace MarketWizard.Application.Features.Portfolios.UpdatePortfolio;

public class UpdatePortfolioInputDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public Guid UserId { get; set; }
    
    public string Description { get; set; }
    
    public string ImageUrl { get; set; }
}