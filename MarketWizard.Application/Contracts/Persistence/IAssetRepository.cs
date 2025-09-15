using MarketWizard.Domain.Entities;

namespace MarketWizard.Application.Contracts.Persistence;

public interface IWatchlistRepository : IGenericRepository<Watchlist>
{
    Task<IEnumerable<Watchlist>> GetAllWithAssets(CancellationToken cancellationToken);
    
    Task<IEnumerable<Asset>> GetAllWithPriceHistories(CancellationToken cancellationToken);
}