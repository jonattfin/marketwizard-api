using MarketWizard.AI.Services;
using MarketWizard.Application.Contracts.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.AI;

public static class AIServiceRegistration
{
    public static IServiceCollection AddAIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISwotService, SwotService>();
        
        return services;
    }
}