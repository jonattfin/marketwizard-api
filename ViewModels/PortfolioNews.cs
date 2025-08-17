namespace MarketWizardApi.ViewModels;

public class PortfolioNews
{
    public Guid Id { get; set; }
    
    public Guid PortfolioId { get; set; }

    public string Time { get; set; }
    
    public string Symbol { get; set; }
    
    public string Headline { get; set; }
    
    public string Provider {get; set; }
}