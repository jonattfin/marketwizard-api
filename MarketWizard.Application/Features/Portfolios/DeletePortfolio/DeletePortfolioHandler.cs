using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;
using MediatR;

namespace MarketWizard.Application.Features.Portfolios.DeletePortfolio;

public record DeletePortfolioCommand(Guid PortfolioId) : ICommand<bool>;

public class PortfolioDeletedEvent
{
    public Guid PortfolioId { get; set; }
}

public class DeletePortfolioHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePortfolioCommand, bool>
{
    public async Task<bool> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.PortfolioRepository.Delete(request.PortfolioId, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        return true;
    }
}