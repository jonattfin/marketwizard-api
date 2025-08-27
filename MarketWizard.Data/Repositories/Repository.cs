using MarketWizard.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class Repository(MarketWizardContext context) : IRepository
{
    public async Task<Guid> AddPortfolio(Portfolio portfolio)
    {
        await context.Portfolios.AddAsync(portfolio);
        await context.SaveChangesAsync();
        
        return portfolio.Id;
    }

    public IQueryable<Asset> GetAssets(CancellationToken cancellationToken)
        => context.Assets.OrderByDescending(a => a.Name)
            .Include(a => a.PriceHistories);

    public IQueryable<Portfolio> GetPortfolios(CancellationToken cancellationToken)
    {
        return context.Portfolios.OrderByDescending(p => p.Name);
    }

    public async Task<Portfolio?> GetPortfolioById(Guid portfolioId, CancellationToken cancellationToken)
    {
        var p = await context.Portfolios
            .Include(p => p.PortfolioAssets)
            .ThenInclude(pa => pa.Asset)
            .ThenInclude(pa => pa.PriceHistories)
            .FirstOrDefaultAsync(p => p.Id == portfolioId, cancellationToken);

        return p;
    }
}