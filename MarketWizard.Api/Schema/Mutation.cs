
using MarketWizard.Application;
using MarketWizard.Domain;
using MarketWizardApi.Dto.Inputs;
using MarketWizardApi.Dto.Outputs;

namespace MarketWizardApi.Schema;

public class Mutation
{
    public async Task<PortfolioOutput> AddPortfolio(PortfolioInput portfolio, IPortfolioService portfolioService)
    {
        var portfolioEntity = MapPortfolioInput(portfolio);
        var portfolioId = await portfolioService.AddPortfolio(portfolioEntity);
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