using MarketWizard.Application.Interfaces.Persistence;
using MarketWizard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class WatchlistRepository(MarketWizardContext context) : GenericRepository<Watchlist>(context), IWatchlistRepository
{
    private readonly MarketWizardContext _context = context;

    public async Task<IEnumerable<Watchlist>> GetAllWithAssets(CancellationToken cancellationToken)
    {
        return await context.Watchlists.Include(watchlist => watchlist.Assets).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Asset>> GetAllWithPriceHistories(Guid userId, CancellationToken cancellationToken)
    {
        var watchlist = await context.Watchlists.Include(watchlist => watchlist.Assets)
            .FirstAsync(x => x.UserId == userId, cancellationToken);

        return watchlist?.Assets ?? [];
    }
}
