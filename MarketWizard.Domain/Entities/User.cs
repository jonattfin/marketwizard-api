using MarketWizard.Domain.Entities.Common;

namespace MarketWizard.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }
}