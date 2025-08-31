namespace MarketWizard.Application.Dto;

public class AddPortfolioInputDto
{
    public string Name { get; set; }
    
    public Guid UserId { get; set; }
    
    public string Description { get; set; }
    
    public string ImageUrl { get; set; }
}