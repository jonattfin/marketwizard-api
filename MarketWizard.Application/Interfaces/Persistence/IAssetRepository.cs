using MarketWizard.Domain.Entities;

namespace MarketWizard.Application.Interfaces.Persistence;

public interface IWatchlistRepository : IGenericRepository<Watchlist>
{
    Task<IEnumerable<Asset>> GetAllWithPriceHistories(Guid userId, CancellationToken cancellationToken);
}