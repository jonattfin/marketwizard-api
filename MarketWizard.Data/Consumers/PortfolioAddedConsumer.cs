using MarketWizard.Application.Features.Portfolios.AddPortfolio;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Data.Consumers;

public abstract class PortfolioAddedConsumer(ILogger<PortfolioAddedConsumer> logger) : IConsumer<PortfolioAddedEvent>
{
  public Task Consume(ConsumeContext<PortfolioAddedEvent> context)
  {
    // TODO - Save this in DB
    logger.LogInformation("Consumed event: " + context.Message.PortfolioId);
    
    return Task.CompletedTask;
  }
}
