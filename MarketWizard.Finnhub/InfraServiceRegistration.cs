
using MarketWizard.Application.Interfaces.Infra;
using MarketWizard.Finnhub.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.Finnhub;

public static class InfraServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        
        services.AddHostedService<StockPriceBackgroundService>();
        
        services.AddSingleton<IFinnhubService, FinnhubService>();
        
        return services;
    }
}