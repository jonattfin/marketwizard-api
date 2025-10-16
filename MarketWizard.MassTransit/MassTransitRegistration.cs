using MarketWizard.MassTransit.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketWizard.MassTransit;

public static class MassTransitRegistration
{
  public static IServiceCollection AddMassTransitServices(this IServiceCollection services,
    IConfiguration configuration)
  {
    // TODO - Maybe we don't need this, as we've implemented in with the outbox pattern?
    // services.AddMassTransit((x) =>
    // {
    //   x.AddConsumer<AddPortfolioConsumer>();
    //   x.AddConsumer<UpdatePortfolioConsumer>();
    //
    //   x.UsingRabbitMq((ctx, cfg) =>
    //   {
    //     cfg.Host("localhost", "/", h =>
    //     {
    //       h.Username("myUser");
    //       h.Password("myPassword");
    //     });
    //
    //     cfg.ReceiveEndpoint("add-portfolio_queue", e => { e.ConfigureConsumer<AddPortfolioConsumer>(ctx); });
    //
    //     cfg.ReceiveEndpoint("update-portfolio_queue", e => { e.ConfigureConsumer<UpdatePortfolioConsumer>(ctx); });
    //   });
    // });

    return services;
  }
}