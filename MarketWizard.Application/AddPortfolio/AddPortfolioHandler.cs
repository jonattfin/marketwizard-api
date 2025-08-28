using HotChocolate.Subscriptions;
using MarketWizard.Application.Messaging.Abstractions;
using MarketWizard.Data.Repositories;
using MarketWizard.Domain.Entities;
using MediatR;

namespace MarketWizard.Application.AddPortfolio;

public record AddPortfolioCommand(Portfolio Portfolio) : ICommand<Guid>;

public class AddPortfolioHandler(IRepository repository, ITopicEventSender sender)
    : IRequestHandler<AddPortfolioCommand, Guid>
{
    public async Task<Guid> Handle(AddPortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolioId = await repository.AddPortfolio(request.Portfolio, cancellationToken);;
        await sender.SendAsync("PortfolioAdded", request.Portfolio, cancellationToken);

        return portfolioId;
    }
}