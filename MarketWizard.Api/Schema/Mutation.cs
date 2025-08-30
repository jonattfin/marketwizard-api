
using MarketWizard.Application.Features.AddPortfolio;
using MarketWizard.Application.Features.AddPortfolio.Dto;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Mutation
{
    public async Task<PortfolioOutput> AddPortfolio(PortfolioInput portfolioInput, [FromServices] IMediator mediator)
    {
        return await mediator.Send(new AddPortfolioCommand(portfolioInput));
    }
}