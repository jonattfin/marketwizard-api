using HotChocolate.Subscriptions;
using MarketWizard.Application.Features.Portfolios.DeletePortfolio;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Data.Consumers;

public abstract class PortfolioDeletedConsumer(ITopicEventSender sender, ILogger<PortfolioDeletedConsumer> logger)
    : IConsumer<PortfolioDeletedEvent>
{
    public async Task Consume(ConsumeContext<PortfolioDeletedEvent> context)
    {
        // publish the subscription event (graphql subscription)
        await sender.SendAsync("PortfolioDeleted", context.Message.PortfolioId, context.CancellationToken);
    }
}