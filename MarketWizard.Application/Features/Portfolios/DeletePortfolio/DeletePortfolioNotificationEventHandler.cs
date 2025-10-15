using HotChocolate.Subscriptions;
using MassTransit;
using MediatR;

namespace MarketWizard.Application.Features.Portfolios.DeletePortfolio;

public class DeletePortfolioNotification : INotification
{
  public Guid PortfolioId { get; set; }
}

public class DeletePortfolioMessage
{
  public Guid PortfolioId { get; set; }
}

public class DeletePortfolioNotificationEventHandler(ITopicEventSender sender, IPublishEndpoint publishEndpoint)
  : INotificationHandler<DeletePortfolioNotification>
{
  public async Task Handle(DeletePortfolioNotification notification, CancellationToken cancellationToken)
  {
    await sender.SendAsync("PortfolioDeleted", notification.PortfolioId, cancellationToken);

    await publishEndpoint.Publish(new DeletePortfolioMessage() { PortfolioId = notification.PortfolioId },
      cancellationToken);
  }
}