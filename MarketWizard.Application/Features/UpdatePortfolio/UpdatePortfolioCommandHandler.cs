using HotChocolate.Subscriptions;
using Mapster;
using MarketWizard.Application.Dto;
using MarketWizard.Application.Interfaces.Cqrs;
using MarketWizard.Application.Interfaces.Persistence;
using MarketWizard.Domain.Entities;
using MediatR;

namespace MarketWizard.Application.Features.UpdatePortfolio;

public record UpdatePortfolioCommand(UpdatePortfolioInputDto UpdatePortfolio) : ICommand<UpdatePortfolioOutputDto>;

public class UpdatePortfolioHandler(IUnitOfWork unitOfWork, ITopicEventSender sender)
    : IRequestHandler<UpdatePortfolioCommand, UpdatePortfolioOutputDto>
{
    public async Task<UpdatePortfolioOutputDto> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolioEntity = await unitOfWork.PortfolioRepository.GetById(request.UpdatePortfolio.Id, cancellationToken);

        if (portfolioEntity is null)
        {
            throw new ApplicationException("Portfolio not found");
        }
        
        request.UpdatePortfolio.Adapt(portfolioEntity);
        
        unitOfWork.PortfolioRepository.Update(portfolioEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await sender.SendAsync("PortfolioUpdated", request.UpdatePortfolio, cancellationToken);

        return new UpdatePortfolioOutputDto() { Id = request.UpdatePortfolio.Id };
    }
}