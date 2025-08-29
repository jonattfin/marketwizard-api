using HotChocolate.Subscriptions;
using MarketWizard.Application.Interfaces;
using MarketWizard.Domain.Entities;
using MediatR;

namespace MarketWizard.Application.Features.AddPortfolio;

public record AddPortfolioCommand(Portfolio Portfolio) : ICommand<Guid>;

public class AddPortfolioHandler(IUnitOfWork unitOfWork, ITopicEventSender sender)
    : IRequestHandler<AddPortfolioCommand, Guid>
{
    public async Task<Guid> Handle(AddPortfolioCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.PortfolioRepository.Insert(request.Portfolio, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await sender.SendAsync("PortfolioAdded", request.Portfolio, cancellationToken);
        
        return Guid.NewGuid();
    }
}