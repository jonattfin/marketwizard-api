using MassTransit;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Application.Features.Portfolios.UpdatePortfolio;

public class UpdatePortfolioConsumer(ILogger<UpdatePortfolioConsumer> logger) : IConsumer<UpdatePortfolioMessage>
{
  public Task Consume(ConsumeContext<UpdatePortfolioMessage> context)
  {
    var message = context.Message.PortfolioId;
    
    logger.LogInformation("Consuming message: " + message);
    
    return Task.CompletedTask;
  }
}