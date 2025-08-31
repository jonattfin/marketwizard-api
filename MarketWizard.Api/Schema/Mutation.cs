
using MarketWizard.Application.Dto;
using MarketWizard.Application.Features.AddPortfolio;
using MarketWizard.Application.Features.DeletePortfolio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Mutation
{
    public async Task<AddPortfolioOutputDto> AddPortfolio(AddPortfolioInputDto addPortfolioInputDto, [FromServices] IMediator mediator)
    {
        return await mediator.Send(new AddPortfolioCommand(addPortfolioInputDto));
    }
    
    public async Task<bool> DeletePortfolio(Guid portfolioId, [FromServices] IMediator mediator)
    {
        return await mediator.Send(new DeletePortfolioCommand(portfolioId));
    }
}