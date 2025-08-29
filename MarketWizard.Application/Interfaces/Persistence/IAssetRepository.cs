using MarketWizard.Domain.Entities;

namespace MarketWizard.Application.Interfaces.Persistence;

public interface IAssetRepository : IGenericRepository<Asset>
{
    IQueryable<Asset> GetAllWithPriceHistories(CancellationToken cancellationToken);
}