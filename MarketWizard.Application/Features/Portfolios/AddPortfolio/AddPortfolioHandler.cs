using Mapster;
using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities;
using MediatR;

namespace MarketWizard.Application.Features.Portfolios.AddPortfolio;

public class PortfolioAddedEvent
{
    public Guid PortfolioId { get; set; }
}

public record AddPortfolioCommand(AddPortfolioInputDto AddPortfolio) : ICommand<AddPortfolioOutputDto>;

public class AddPortfolioHandler(IUnitOfWork unitOfWork, IMediator mediator)
    : IRequestHandler<AddPortfolioCommand, AddPortfolioOutputDto>
{
    public async Task<AddPortfolioOutputDto> Handle(AddPortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolioEntity = request.AddPortfolio.Adapt<Portfolio>();
        portfolioEntity.Id = Guid.NewGuid();

        await unitOfWork.PortfolioRepository.Insert(portfolioEntity, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        await mediator.Publish(new AddPortfolioNotification() { Portfolio = portfolioEntity }, cancellationToken);

        return new AddPortfolioOutputDto() { Id = portfolioEntity.Id };
    }
}