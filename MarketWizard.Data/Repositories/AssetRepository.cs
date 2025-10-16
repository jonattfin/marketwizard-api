using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class WatchlistRepository(MarketWizardContext context) : GenericRepository<Watchlist>(context), IWatchlistRepository
{
    public async Task<IEnumerable<Watchlist>> GetAllWithAssets(CancellationToken cancellationToken)
    {
        return await DbSet
            .Include(watchlist => watchlist.Assets).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Asset>> GetAllWithPriceHistories(CancellationToken cancellationToken)
    {
        var watchlist = await DbSet.Include(watchlist => watchlist.Assets)
            .FirstOrDefaultAsync(cancellationToken);

        return watchlist?.Assets ?? [];
    }
}
