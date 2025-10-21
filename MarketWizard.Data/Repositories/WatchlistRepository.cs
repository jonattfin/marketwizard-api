using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class WatchlistRepository(MarketWizardContext context)
    : GenericRepository<Watchlist>(context), IWatchlistRepository
{
    public async Task<IEnumerable<Watchlist>> GetAllForUser(Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<Watchlist?> GetByIdWithAssets(Guid watchlistId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(x => x.Id == watchlistId)
            .Include(watchlist => watchlist.Assets)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Watchlist>> GetAllWithAssets(Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(x => x.UserId == userId)
            .Include(watchlist => watchlist.Assets).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Asset>> GetAllWithPriceHistories(Guid userId, Guid watchlistId, CancellationToken cancellationToken)
    {
        var watchlist = await DbSet
            .Where(x => x.UserId == userId && x.Id == watchlistId)
            .Include(watchlist => watchlist.Assets)
            .FirstOrDefaultAsync(cancellationToken);

        return watchlist?.Assets ?? [];
    }
}