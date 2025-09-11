using HotChocolate.Subscriptions;
using Mapster;
using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Dto;
using MarketWizard.Domain.Entities;
using MediatR;

namespace MarketWizard.Application.Features.AddPortfolio;

public record AddPortfolioCommand(AddPortfolioInputDto AddPortfolio) : ICommand<AddPortfolioOutputDto>;

public class AddPortfolioHandler(IUnitOfWork unitOfWork, ITopicEventSender sender)
    : IRequestHandler<AddPortfolioCommand, AddPortfolioOutputDto>
{
    public async Task<AddPortfolioOutputDto> Handle(AddPortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolioEntity = request.AddPortfolio.Adapt<Portfolio>();
        
        await unitOfWork.PortfolioRepository.Insert(portfolioEntity, cancellationToken);
        await unitOfWork.Commit(cancellationToken);
        
        await sender.SendAsync("PortfolioAdded", request.AddPortfolio, cancellationToken);

        return new AddPortfolioOutputDto() { Id = Guid.NewGuid() }; // TODO - Fetch the request portfolio id
    }
}