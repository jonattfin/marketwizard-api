
using MarketWizard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class AssetRepository(MarketWizardContext context) : GenericRepository<Asset>(context)
{
    private readonly MarketWizardContext _context = context;

    public override IQueryable<Asset> Get(CancellationToken cancellationToken = default)
    {
        return _context.Assets.OrderByDescending(a => a.Name)
            .Include(a => a.PriceHistories);
    }
}
