using HotChocolate.Subscriptions;
using Mapster;
using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Features.AddPortfolio;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Application.Features.UpdatePortfolio;

public record UpdatePortfolioCommand(UpdatePortfolioInputDto UpdatePortfolio) : ICommand<UpdatePortfolioOutputDto>;

public class UpdatePortfolioHandler(IUnitOfWork unitOfWork, ITopicEventSender sender, ILogger<UpdatePortfolioHandler> logger)
    : IRequestHandler<UpdatePortfolioCommand, UpdatePortfolioOutputDto>
{
    public async Task<UpdatePortfolioOutputDto> Handle(UpdatePortfolioCommand request,
        CancellationToken cancellationToken)
    {
        var portfolioEntity =
            await unitOfWork.PortfolioRepository.GetById(request.UpdatePortfolio.Id, cancellationToken);

        if (portfolioEntity is null)
        {
            throw new ApplicationException("Portfolio not found");
        }

        request.UpdatePortfolio.Adapt(portfolioEntity);

        try
        {
            unitOfWork.PortfolioRepository.Update(portfolioEntity);
            await unitOfWork.Commit(cancellationToken);

            await sender.SendAsync("PortfolioUpdated", request.UpdatePortfolio.Id, cancellationToken);
        }
        catch (Exception e)
        {
            await unitOfWork.Rollback(cancellationToken);
            
            logger.LogError(e, "Error updating portfolio");
            throw;
        }


        return new UpdatePortfolioOutputDto() { Id = request.UpdatePortfolio.Id };
    }
}