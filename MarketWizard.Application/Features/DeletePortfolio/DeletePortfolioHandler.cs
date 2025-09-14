using HotChocolate.Subscriptions;
using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Application.Features.DeletePortfolio;

public record DeletePortfolioCommand(Guid PortfolioId) : ICommand<bool>;

public class DeletePortfolioHandler(
    IUnitOfWork unitOfWork, ITopicEventSender sender)
    : IRequestHandler<DeletePortfolioCommand, bool>
{
    public async Task<bool> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.PortfolioRepository.Delete(request.PortfolioId, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        await sender.SendAsync("PortfolioDeleted", request.PortfolioId, cancellationToken);

        return true;
    }
}