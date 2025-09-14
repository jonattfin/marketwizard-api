using HotChocolate.Subscriptions;
using Mapster;
using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Application.Features.AddPortfolio;

public record AddPortfolioCommand(AddPortfolioInputDto AddPortfolio) : ICommand<AddPortfolioOutputDto>;

public class AddPortfolioHandler(IUnitOfWork unitOfWork, ITopicEventSender sender, ILogger<AddPortfolioHandler> logger)
    : IRequestHandler<AddPortfolioCommand, AddPortfolioOutputDto>
{
    public async Task<AddPortfolioOutputDto> Handle(AddPortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolioEntity = request.AddPortfolio.Adapt<Portfolio>();

        try
        {
            await unitOfWork.PortfolioRepository.Insert(portfolioEntity, cancellationToken);
            await unitOfWork.Commit(cancellationToken);

            await sender.SendAsync("PortfolioAdded", request.AddPortfolio, cancellationToken);
        }
        catch (Exception e)
        {
            await unitOfWork.Rollback(cancellationToken);
            
            logger.LogError(e, "Error adding portfolio");
            throw;
        }

        return new AddPortfolioOutputDto() { Id = Guid.NewGuid() }; // TODO - Fetch the request portfolio id
    }
}