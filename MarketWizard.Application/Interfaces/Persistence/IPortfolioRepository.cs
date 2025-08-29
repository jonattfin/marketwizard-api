using MarketWizard.Domain.Entities;

namespace MarketWizard.Application.Interfaces.Persistence;

public interface IPortfolioRepository : IGenericRepository<Portfolio>
{
    public Task<Portfolio?> GetByIdWithRelatedEntities(object id, CancellationToken cancellationToken = default);
}