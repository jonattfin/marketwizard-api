
namespace MarketWizard.Application.Contracts.Infra;

public interface IUserService
{
    Guid GetAuthenticatedUserId();
}