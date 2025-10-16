using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Data.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.Data;

public class RabbitMQConfiguration
{
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MarketWizardConnection");
        services.AddDbContext<MarketWizardContext>(options => options.UseNpgsql(connectionString));

        services.AddHealthChecks().AddNpgSql(connectionString!);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        ConfigureMassTransit(services, configuration);

        return services;
    }

    private static void ConfigureMassTransit(IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfiguration = configuration
            .GetSection(nameof(RabbitMQConfiguration))
            .Get<RabbitMQConfiguration>();

        services.AddMassTransit(busConfig =>
        {
            busConfig.AddEntityFrameworkOutbox<MarketWizardContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(30);
                o.UsePostgres().UseBusOutbox();
            });

            busConfig.SetKebabCaseEndpointNameFormatter();

            // busConfig.AddConsumer<PortfolioAddedConsumer>();
            // .Endpoint(c => c.InstanceId = ServiceName); // TODO - fix this

            busConfig.AddConfigureEndpointsCallback((context, name, cfg) =>
            {
                cfg.UseEntityFrameworkOutbox<MarketWizardContext>(context);
            });

            busConfig.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(rabbitMqConfiguration.Host), h =>
                {
                    h.Username(rabbitMqConfiguration.Username);
                    h.Password(rabbitMqConfiguration.Password);
                });

                cfg.UseMessageRetry(r =>
                    r.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5)));

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}