using MarketWizard.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.Data;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MarketWizardConnection");
        services.AddDbContext<MarketWizardContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IRepository, Repository>();
        
        return services;
    }
}