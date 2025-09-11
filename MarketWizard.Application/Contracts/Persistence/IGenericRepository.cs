using MarketWizard.Domain.Entities.Common;

namespace MarketWizard.Application.Contracts.Persistence;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> Get(CancellationToken cancellationToken = default);
    
    Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default);
    
    Task Insert(TEntity entity, CancellationToken cancellationToken = default);
    
    Task Delete(Guid id, CancellationToken cancellationToken = default);
    
    void Delete(TEntity entityToDelete);
    
    void Update(TEntity entityToUpdate);
}