using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Finnhub.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.Finnhub;

public static class InfraServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IFinnhubService, FinnhubService>(client =>
        {
            client.BaseAddress = new Uri("https://finnhub.io/api/v1/");
        }).AddStandardResilienceHandler();
        
        services.AddHostedService<StockPriceBackgroundService>();
        
        return services;
    }
}