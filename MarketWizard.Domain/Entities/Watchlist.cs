using MarketWizard.Domain.Entities.Interfaces;

namespace MarketWizard.Domain.Entities;

public class Watchlist : IEntity
{
    public Guid Id { get; set; }
    
    public User User { get; set; }
    
    public Guid UserId { get; set; }
    
    public ICollection<Asset> Assets { get; set; } = new List<Asset>();
}