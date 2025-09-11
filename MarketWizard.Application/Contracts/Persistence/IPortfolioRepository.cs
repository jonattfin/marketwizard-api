using MarketWizard.Domain.Entities;

namespace MarketWizard.Application.Contracts.Persistence;

public interface IPortfolioRepository : IGenericRepository<Portfolio>
{
    public IQueryable<Portfolio> GetAllWithRelatedEntities(CancellationToken cancellationToken = default);
    
    public Task<Portfolio?> GetByIdWithRelatedEntities(object id, CancellationToken cancellationToken = default);
}