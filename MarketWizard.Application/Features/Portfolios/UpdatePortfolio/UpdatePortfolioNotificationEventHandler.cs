using HotChocolate.Subscriptions;
using MassTransit;
using MediatR;

namespace MarketWizard.Application.Features.Portfolios.UpdatePortfolio;

public class UpdatePortfolioNotification : INotification
{
  public Guid PortfolioId { get; set; }
}

public class UpdatePortfolioMessage
{
  public Guid PortfolioId { get; set; }
}

public class UpdatePortfolioNotificationEventHandler(ITopicEventSender sender, IPublishEndpoint publishEndpoint)
  : INotificationHandler<UpdatePortfolioNotification>
{
  public async Task Handle(UpdatePortfolioNotification notification, CancellationToken cancellationToken)
  {
    await sender.SendAsync("PortfolioUpdated", notification.PortfolioId, cancellationToken);
    
    await publishEndpoint.Publish(new UpdatePortfolioMessage() {PortfolioId = notification.PortfolioId}, cancellationToken);
  }
}