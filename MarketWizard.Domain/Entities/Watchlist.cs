using MarketWizard.Domain.Entities.Common;

namespace MarketWizard.Domain.Entities;

public class Watchlist : BaseEntity, IUserScopedEntity
{
    public Guid Id { get; set; }
    
    public User User { get; set; }
    
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public ICollection<Asset> Assets { get; set; } = new List<Asset>();
}