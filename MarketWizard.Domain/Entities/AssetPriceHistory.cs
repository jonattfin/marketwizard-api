using MarketWizard.Domain.Entities.Interfaces;

namespace MarketWizard.Domain.Entities;

public class AssetPriceHistory : IEntity
{
    public Guid Id { get; set; }
    
    public Asset Asset { get; set; }
    
    public Guid AssetId { get; set; }
    
    public double Price { get; set; }
    
    public DateTime Date { get; set; }
}