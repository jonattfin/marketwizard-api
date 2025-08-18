using MarketWizard.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class Repository(MarketWizardContext context) : IRepository
{
    public IQueryable<Portfolio> GetPortfolios(CancellationToken cancellationToken)
    {
        return context.Portfolios.OrderByDescending(p => p.Name);
    }

    public async Task<Portfolio?> GetPortfolioById(Guid portfolioId, CancellationToken cancellationToken)
    {
        return await context.Portfolios.FirstOrDefaultAsync(p => p.Id == portfolioId, cancellationToken);
    }

    public async Task<IQueryable<PortfolioNews>> GetPortfolioNewsById(Guid portfolioId, CancellationToken cancellationToken)
    {
        var portfolio = await context.Portfolios
            .Include(portfolio => portfolio.PortfolioNews)
            .FirstAsync(p => p.Id == portfolioId, cancellationToken);

        return portfolio?.PortfolioNews ?? Enumerable.Empty<PortfolioNews>().AsQueryable();
    }
    
    public async Task<IQueryable<PortfolioAsset>> GetPortfolioAssetsById(Guid portfolioId, CancellationToken cancellationToken)
    {
         var portfolio = await context.Portfolios
            .Include(portfolio => portfolio.PortfolioAssets)
            .FirstAsync(p => p.Id == portfolioId, cancellationToken);

        return portfolio.PortfolioAssets ?? Enumerable.Empty<PortfolioAsset>().AsQueryable();
    }
}