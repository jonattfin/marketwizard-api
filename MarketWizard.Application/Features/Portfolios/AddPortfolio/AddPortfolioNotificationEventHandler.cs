using HotChocolate.Subscriptions;
using MarketWizard.Domain.Entities;
using MassTransit;
using MediatR;

namespace MarketWizard.Application.Features.Portfolios.AddPortfolio;

public class AddPortfolioNotification : INotification
{
  public Portfolio Portfolio { get; set; }
}

public class AddPortfolioMessage : INotification
{
  public Portfolio Portfolio { get; set; }
}

public class AddPortfolioNotificationEventHandler(ITopicEventSender sender, IPublishEndpoint publishEndpoint)
  : INotificationHandler<AddPortfolioNotification>
{

  public async Task Handle(AddPortfolioNotification notification, CancellationToken cancellationToken)
  {
     await sender.SendAsync("PortfolioAdded", notification.Portfolio, cancellationToken);

     await publishEndpoint.Publish(new AddPortfolioMessage() {Portfolio = notification.Portfolio}, cancellationToken);
  }
}