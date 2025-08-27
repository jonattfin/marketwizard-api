using MarketWizard.Data.Repositories;
using MarketWizard.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi;

public class PortfolioInput
{
    public string Name { get; set; }
    
    public Guid UserId { get; set; }
    
    public string Description { get; set; }
    
    public string ImageUrl { get; set; }
}

public class PortfolioOutput
{
    public Guid Id { get; set; }
    
}

public class Mutation
{
    public async Task<PortfolioOutput> AddPortfolio([FromServices] IRepository repository, PortfolioInput portfolio)
    {
        var portfolioEntity = new Portfolio
        {
            Name = portfolio.Name,
            UserId = portfolio.UserId,
            Description = portfolio.Description,
            ImageUrl = portfolio.ImageUrl
        };
        
        var portfolioId = await repository.AddPortfolio(portfolioEntity);

        return new PortfolioOutput { Id = portfolioId };
    }
}