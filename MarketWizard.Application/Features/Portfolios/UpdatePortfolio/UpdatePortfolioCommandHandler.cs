using Mapster;
using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Exceptions;
using MediatR;

namespace MarketWizard.Application.Features.Portfolios.UpdatePortfolio;

public record UpdatePortfolioCommand(UpdatePortfolioInputDto UpdatePortfolio) : ICommand<UpdatePortfolioOutputDto>;

public class UpdatePortfolioHandler(
    IUnitOfWork unitOfWork)
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
        // portfolioEntity.Version = Guid.NewGuid(); // TODO - Revisit versioning

        unitOfWork.PortfolioRepository.Update(portfolioEntity);
        await unitOfWork.Commit(cancellationToken);

        return new UpdatePortfolioOutputDto() { Id = request.UpdatePortfolio.Id };
    }
}