namespace MarketWizard.Domain;

public class AssetNews
{
    public Guid Id { get; set; }
    
    public Asset Asset { get; set; }
    
    public Guid AssetId { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Symbol { get; set; }
    
    public string Headline { get; set; }
    
    public string Provider {get; set; }
}