
using Mapster;
using MarketWizard.Application.Features.AddPortfolio;
using MarketWizard.Domain.Entities;
using MarketWizardApi.Dto.Inputs;
using MarketWizardApi.Dto.Outputs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Mutation
{
    public async Task<PortfolioOutput> AddPortfolio(PortfolioInput portfolioInput, [FromServices] IMediator mediator)
    {
        var portfolioEntity = portfolioInput.Adapt<Portfolio>();
        
        var portfolioId = await mediator.Send(new AddPortfolioCommand(portfolioEntity));
        
        return portfolioId.Adapt<PortfolioOutput>();
    }
    
}