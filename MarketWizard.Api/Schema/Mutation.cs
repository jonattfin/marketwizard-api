using MarketWizard.Application.Features.AddPortfolio;
using MarketWizard.Application.Features.DeletePortfolio;
using MarketWizard.Application.Features.UpdatePortfolio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Mutation
{
    public async Task<AddPortfolioOutputDto> AddPortfolio(AddPortfolioInputDto portfolioInput, [FromServices] IMediator mediator)
    {
        return await mediator.Send(new AddPortfolioCommand(portfolioInput));
    }
    
    public async Task<UpdatePortfolioOutputDto> UpdatePortfolio(UpdatePortfolioInputDto portfolioInput, [FromServices] IMediator mediator)
    {
        return await mediator.Send(new UpdatePortfolioCommand(portfolioInput));
    }
    
    public async Task<bool> DeletePortfolio(Guid portfolioId, [FromServices] IMediator mediator)
    {
        return await mediator.Send(new DeletePortfolioCommand(portfolioId));
    }
}