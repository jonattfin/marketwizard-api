using HotChocolate.Subscriptions;
using MarketWizard.Application.Features.Portfolios.AddPortfolio;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Data.Consumers;

public abstract class PortfolioAddedConsumer(ITopicEventSender sender, ILogger<PortfolioAddedConsumer> logger)
    : IConsumer<PortfolioAddedEvent>
{
    public async Task Consume(ConsumeContext<PortfolioAddedEvent> context)
    {
        // publish the subscription event (graphql subscription)
        await sender.SendAsync("PortfolioAdded", context.Message.PortfolioId, context.CancellationToken);
    }
}