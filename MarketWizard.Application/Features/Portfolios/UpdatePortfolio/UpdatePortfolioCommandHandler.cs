using HotChocolate.Subscriptions;
using Mapster;
using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Exceptions;
using MediatR;

namespace MarketWizard.Application.Features.Portfolios.UpdatePortfolio;

public record UpdatePortfolioCommand(UpdatePortfolioInputDto UpdatePortfolio) : ICommand<UpdatePortfolioOutputDto>;

public class UpdatePortfolioHandler(
    IUnitOfWork unitOfWork,
    ITopicEventSender sender)
    : IRequestHandler<UpdatePortfolioCommand, UpdatePortfolioOutputDto>
{
    public async Task<UpdatePortfolioOutputDto> Handle(UpdatePortfolioCommand request,
        CancellationToken cancellationToken)
    {
        var portfolioEntity =
            await unitOfWork.PortfolioRepository.GetById(request.UpdatePortfolio.Id, cancellationToken);

        if (portfolioEntity is null)
        {
            throw new PortfolioNotFoundException(request.UpdatePortfolio.Id);
        }

        request.UpdatePortfolio.Adapt(portfolioEntity);

        unitOfWork.PortfolioRepository.Update(portfolioEntity);
        await unitOfWork.Commit(cancellationToken);

        await sender.SendAsync("PortfolioUpdated", request.UpdatePortfolio.Id, cancellationToken);

        return new UpdatePortfolioOutputDto() { Id = request.UpdatePortfolio.Id };
    }
}