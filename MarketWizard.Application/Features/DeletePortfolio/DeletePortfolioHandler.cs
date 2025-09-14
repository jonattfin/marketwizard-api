using HotChocolate.Subscriptions;
using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Application.Features.DeletePortfolio;

public record DeletePortfolioCommand(Guid PortfolioId) : ICommand<bool>;

public class DeletePortfolioHandler(IUnitOfWork unitOfWork, ITopicEventSender sender, ILogger<DeletePortfolioHandler> logger)
    : IRequestHandler<DeletePortfolioCommand, bool>
{
    public async Task<bool> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.PortfolioRepository.Delete(request.PortfolioId, cancellationToken);
            await unitOfWork.Commit(cancellationToken);

            await sender.SendAsync("PortfolioDeleted", request.PortfolioId, cancellationToken);
        }
        catch (Exception e)
        {
            await unitOfWork.Rollback(cancellationToken);
            
            logger.LogError(e, "Error deleting portfolio");
            throw;
        }

        return true;
    }
}