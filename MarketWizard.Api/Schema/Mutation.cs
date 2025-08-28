
using MarketWizard.Application;
using MarketWizard.Application.AddPortfolio;
using MarketWizard.Domain;
using MarketWizard.Domain.Entities;
using MarketWizardApi.Dto.Inputs;
using MarketWizardApi.Dto.Outputs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Mutation
{
    public async Task<PortfolioOutput> AddPortfolio(PortfolioInput portfolio, [FromServices] IMediator mediator)
    {
        var portfolioEntity = MapPortfolioInput(portfolio);
        
        var portfolioId = await mediator.Send(new AddPortfolioCommand(portfolioEntity));
        
        return MapPortfolioOutput(portfolioId);
    }
    
    private static Portfolio MapPortfolioInput(PortfolioInput portfolio)
    {
        return new Portfolio
        {
            Name = portfolio.Name,
            UserId = portfolio.UserId,
            Description = portfolio.Description,
            ImageUrl = portfolio.ImageUrl
        };
    }

    private static PortfolioOutput MapPortfolioOutput(Guid portfolioId)
    {
         return new PortfolioOutput { Id = portfolioId };
    }
}