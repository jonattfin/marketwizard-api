using MarketWizard.Domain.Entities.Interfaces;

namespace MarketWizard.Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
}