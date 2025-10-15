using FluentValidation;
using MarketWizard.Application.Behaviours;
using MarketWizard.Application.Features.Portfolios.AddPortfolio;
using MarketWizard.Application.Features.Portfolios.UpdatePortfolio;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(ApplicationServiceRegistration).Assembly;
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(currentAssembly));
        
        // add behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));

        services.AddMassTransit((x) =>
        {
            x.AddConsumer<AddPortfolioConsumer>();
            x.AddConsumer<UpdatePortfolioConsumer>();
            
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("myUser");
                    h.Password("myPassword");
                });
                
                cfg.ReceiveEndpoint("add-portfolio_queue", e =>
                {
                    e.ConfigureConsumer<AddPortfolioConsumer>(ctx);
                });
                
                 cfg.ReceiveEndpoint("update-portfolio_queue", e =>
                {
                    e.ConfigureConsumer<UpdatePortfolioConsumer>(ctx);
                });
            });
        });
        
        // add validators
        services.AddValidatorsFromAssembly(currentAssembly);
        
        return services;
    }
}