namespace MarketWizard.Application.Features.AddPortfolio.Dto;

public class PortfolioInput
{
    public string Name { get; set; }
    
    public Guid UserId { get; set; }
    
    public string Description { get; set; }
    
    public string ImageUrl { get; set; }
}