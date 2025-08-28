using HotChocolate.Subscriptions;
using MarketWizard.Data.Repositories;
using MarketWizard.Domain;

namespace MarketWizard.Application;

public interface IPortfolioService
{
    Task<Guid> AddPortfolio(Portfolio portfolio);
}

public class PortfolioService(IRepository repository, ITopicEventSender sender) : IPortfolioService
{
    public async Task<Guid> AddPortfolio(Portfolio portfolio)
    {
        var portfolioId = await repository.AddPortfolio(portfolio);
        await sender.SendAsync("PortfolioAdded", portfolio);

        return portfolioId;
    }
}