using MarketWizard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public abstract class PortfolioRepository(MarketWizardContext context) : GenericRepository<Portfolio>(context)
{
    private readonly MarketWizardContext _context = context;

    public override async Task<Portfolio?> GetById(object id, CancellationToken cancellationToken = default)
    { 
        return await _context.Portfolios
            .Include(p => p.PortfolioAssets)
            .ThenInclude(pa => pa.Asset)
            .ThenInclude(pa => pa.PriceHistories)
            .FirstOrDefaultAsync(p => p.Id == (Guid)id, cancellationToken);
    }
}