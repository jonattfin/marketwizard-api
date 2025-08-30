using HotChocolate.Subscriptions;
using Mapster;
using MarketWizard.Application.Features.AddPortfolio.Dto;
using MarketWizard.Application.Interfaces.Cqrs;
using MarketWizard.Application.Interfaces.Persistence;
using MarketWizard.Domain.Entities;
using MediatR;

namespace MarketWizard.Application.Features.AddPortfolio;

public record AddPortfolioCommand(PortfolioInput Portfolio) : ICommand<PortfolioOutput>;

public class AddPortfolioHandler(IUnitOfWork unitOfWork, ITopicEventSender sender)
    : IRequestHandler<AddPortfolioCommand, PortfolioOutput>
{
    public async Task<PortfolioOutput> Handle(AddPortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolioEntity = request.Portfolio.Adapt<Portfolio>();
        
        await unitOfWork.PortfolioRepository.Insert(portfolioEntity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await sender.SendAsync("PortfolioAdded", request.Portfolio, cancellationToken);

        return new PortfolioOutput() { Id = Guid.NewGuid() }; // TODO - Fetch the request portfolio id
    }
}