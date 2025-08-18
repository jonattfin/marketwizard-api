namespace MarketWizard.Domain;

public class Asset : IEntity
{
    public Guid Id { get; set; }
    
    public string Symbol { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
}
