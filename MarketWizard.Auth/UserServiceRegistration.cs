using MarketWizard.Application.Contracts.Infra;
using MarketWizard.User.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.User;

public static class UserServiceRegistration
{
    public static IServiceCollection AddUserServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUserService, UserService>();

        return services;
    }
}