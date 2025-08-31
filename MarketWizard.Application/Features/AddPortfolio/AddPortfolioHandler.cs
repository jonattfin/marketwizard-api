using HotChocolate.Subscriptions;
using Mapster;
using MarketWizard.Application.Dto;
using MarketWizard.Application.Interfaces.Cqrs;
using MarketWizard.Application.Interfaces.Persistence;
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
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await sender.SendAsync("PortfolioAdded", request.AddPortfolio, cancellationToken);

        return new AddPortfolioOutputDto() { Id = Guid.NewGuid() }; // TODO - Fetch the request portfolio id
    }
}