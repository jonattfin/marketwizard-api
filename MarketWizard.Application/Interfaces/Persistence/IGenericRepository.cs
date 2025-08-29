namespace MarketWizard.Application.Interfaces.Persistence;

public interface IGenericRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Get(CancellationToken cancellationToken = default);
    
    Task<TEntity?> GetById(object id, CancellationToken cancellationToken = default);
    
    Task Insert(TEntity entity, CancellationToken cancellationToken = default);
    
    Task Delete(object id, CancellationToken cancellationToken = default);
    
    void Delete(TEntity entityToDelete);
    
    void Update(TEntity entityToUpdate);
}