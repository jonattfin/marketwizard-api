using MarketWizard.Application.Features.Portfolios.UpdatePortfolio;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MarketWizard.MassTransit.Consumers;

public class UpdatePortfolioConsumer(ILogger<UpdatePortfolioConsumer> logger) : IConsumer<UpdatePortfolioMessage>
{
  public Task Consume(ConsumeContext<UpdatePortfolioMessage> context)
  {
    var message = context.Message.PortfolioId;
    
    logger.LogInformation("Consuming message: " + message);
    
    return Task.CompletedTask;
  }
}