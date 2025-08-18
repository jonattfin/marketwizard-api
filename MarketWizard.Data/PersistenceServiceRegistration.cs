using MarketWizard.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.Data;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        // services.AddDbContext<MarketWizardContext>(options => options.UseInMemoryDatabase(databaseName: "MarketWizard"));
        services.AddDbContext<MarketWizardContext>(options => options.UseSqlite("Data Source=MarketWizard.db"));

        services.AddScoped<IRepository, Repository>();
        
        return services;
    }
}