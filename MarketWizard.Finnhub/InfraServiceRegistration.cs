using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Finnhub.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.Finnhub;

public static class InfraServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var finnhubSection = configuration.GetSection("Finnhub");
        
        services.AddHttpClient<IFinnhubService, FinnhubService>(client =>
        {
            client.BaseAddress = new Uri(finnhubSection.GetValue<string>("BaseUrl")!);
        }).AddStandardResilienceHandler();
        
        services.Decorate<IFinnhubService, FinnhubServiceCacheDecorator>();
        
        services.AddHostedService<StockPriceBackgroundService>();
        
        return services;
    }
}