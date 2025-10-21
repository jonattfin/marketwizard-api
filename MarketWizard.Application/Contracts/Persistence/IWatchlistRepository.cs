using MarketWizard.Domain.Entities;

namespace MarketWizard.Application.Contracts.Persistence;

public interface IWatchlistRepository : IGenericRepository<Watchlist>
{
    Task<Watchlist?> GetByIdWithAssets(Guid watchlistId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Watchlist>> GetAllWithAssets(Guid userId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Watchlist>> GetAllForUser(Guid userId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Asset>> GetAllWithPriceHistories(Guid userId, Guid watchlistId, CancellationToken cancellationToken);
}