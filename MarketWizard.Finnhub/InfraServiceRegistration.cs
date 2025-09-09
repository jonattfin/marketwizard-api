
using MarketWizard.Application.Interfaces.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.Finnhub;

public static class InfraServiceRegistration
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddSingleton<IFinnhubService, FinnhubService>();
        
        return services;
    }
}