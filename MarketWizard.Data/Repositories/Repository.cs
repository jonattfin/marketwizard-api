using MarketWizard.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class Repository(MarketWizardContext context) : IRepository
{
    public IQueryable<Asset> GetAssets(CancellationToken cancellationToken)
        => context.Assets;

    public IQueryable<Portfolio> GetPortfolios(CancellationToken cancellationToken)
    {
        return context.Portfolios
            .Include(p => p.PortfolioAssets)
            .Include(p => p.PortfolioNews)
            .OrderByDescending(p => p.Name);
    }

    public async Task<Portfolio?> GetPortfolioById(Guid portfolioId, CancellationToken cancellationToken)
    {
        var p = await context.Portfolios
            .Include(p => p.PortfolioAssets)
            .Include(p => p.PortfolioNews)
            .FirstOrDefaultAsync(p => p.Id == portfolioId, cancellationToken);

        return p;
    }

    public async Task<PortfolioPerformance?> GetPortfolioPerformanceById(Guid id, CancellationToken cancellationToken)
    {
        var rand = new Random();
        
        var portfolioPerformance = new PortfolioPerformance()
        {
            Id = id,
            Ratio = new PortfolioRatio()
            {
                BetaRatio = 1,
                SharpeRatio = 2,
                SortinoRatio = 3,
            },
            Returns =
            [
                new AssetReturn()
                {
                    AssetName = "DECK",
                    MonthlyReturns = Enumerable.Range(1, 12).Select(i => rand.Next(5, 20) + 0.1).ToArray(),
                    WeeklyReturns = Enumerable.Range(1, 52).Select(i => rand.Next(5, 20) + 0.1).ToArray()
                },
                new AssetReturn()
                {
                    AssetName = "ASML",
                    MonthlyReturns = Enumerable.Range(1, 12).Select(i => rand.Next(5, 20) + 0.1).ToArray(),
                    WeeklyReturns = Enumerable.Range(1, 52).Select(i => rand.Next(5, 20) + 0.1).ToArray()
                },
            ]
        };
        
        return await Task.FromResult(portfolioPerformance);
    }

    public IEnumerable<PortfolioNews> GetPortfolioNewsById(Guid id, CancellationToken cancellationToken)
    {
        var portfolioNews = context.Portfolios.Include(p => p.PortfolioNews)
            .FirstOrDefault(p => p.Id == id)?.PortfolioNews;
        
        return portfolioNews ?? new List<PortfolioNews>();
    }
}