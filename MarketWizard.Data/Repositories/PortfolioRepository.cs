using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class PortfolioRepository(MarketWizardContext context) : GenericRepository<Portfolio>(context), IPortfolioRepository
{
    private readonly MarketWizardContext _context = context;

    public IQueryable<Portfolio> GetAllWithRelatedEntities(CancellationToken cancellationToken = default)
    {
        return _context.Portfolios
            .Include(p => p.PortfolioAssets)
            .ThenInclude(pa => pa.Asset)
            .ThenInclude(pa => pa.PriceHistories);
    }

    public async Task<Portfolio?> GetByIdWithRelatedEntities(object id, CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios
            .Include(p => p.PortfolioAssets)
            .ThenInclude(pa => pa.Asset)
            .ThenInclude(pa => pa.PriceHistories)
            .FirstOrDefaultAsync(p => p.Id == (Guid)id, cancellationToken);
    }
}