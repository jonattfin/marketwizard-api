using MarketWizard.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class Repository(MarketWizardContext context) : IRepository
{
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
}