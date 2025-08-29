using MarketWizard.Application.Interfaces.Persistence;
using MarketWizard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class AssetRepository(MarketWizardContext context) : GenericRepository<Asset>(context), IAssetRepository
{
    private readonly MarketWizardContext _context = context;

    public IQueryable<Asset> GetAllWithPriceHistories(CancellationToken cancellationToken)
    {
        return _context.Assets.OrderByDescending(a => a.Name)
            .Include(a => a.PriceHistories);
    }
}