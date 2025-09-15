using MarketWizard.Application.Contracts.Infra;


namespace MarketWizard.User.Services;

public class UserService() : IUserService
{
    public Guid GetAuthenticatedUserId()
    {
        // TODO - Implement this the right way
        return Guid.Parse("62404bb7-35a3-45de-95af-e85ebebb1f6f");
    }
}