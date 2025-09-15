namespace MarketWizard.Domain.Entities.Common;

public interface IUserScopedEntity
{
    Guid UserId { get; set; }
    
    User User { get; set; }
}