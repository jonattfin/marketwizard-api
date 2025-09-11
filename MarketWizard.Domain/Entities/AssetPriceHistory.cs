using MarketWizard.Domain.Entities.Common;

namespace MarketWizard.Domain.Entities;

public class AssetPriceHistory : BaseEntity
{
    public Asset Asset { get; set; }
    
    public Guid AssetId { get; set; }
    
    public double Price { get; set; }
    
    public DateTime Date { get; set; }
}