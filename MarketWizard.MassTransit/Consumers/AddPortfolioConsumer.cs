using MarketWizard.Application.Features.Portfolios.AddPortfolio;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MarketWizard.MassTransit.Consumers;

public class AddPortfolioConsumer(ILogger<AddPortfolioConsumer> logger): IConsumer<AddPortfolioMessage>
{
  public Task Consume(ConsumeContext<AddPortfolioMessage> context)
  {
    var portfolio = context.Message.Portfolio;
    
    logger.LogInformation("Consuming message: " + portfolio.Id);
    
    return Task.CompletedTask;
  }
}