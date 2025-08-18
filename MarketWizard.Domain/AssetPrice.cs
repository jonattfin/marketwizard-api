namespace MarketWizard.Domain;

public class AssetPrice
{
    public Guid Id { get; set; }
    
    public Asset Asset { get; set; }
    
    public Guid AssetId { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime Date { get; set; }
}