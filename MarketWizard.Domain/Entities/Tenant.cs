using MarketWizard.Domain.Entities.Common;

namespace MarketWizard.Domain.Entities;

public class Tenant : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }
    
    public ICollection<User> Users { get; set; } = new List<User>();
}