using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class GenericRepository<TEntity>(MarketWizardContext context) : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual IQueryable<TEntity> Get(CancellationToken cancellationToken = default)
    {
        return _dbSet.AsQueryable(); // TODO Add cancellation token
    }

    public virtual async Task<TEntity?> GetById(object id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    public virtual async Task Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public virtual async Task Delete(object id, CancellationToken cancellationToken = default)
    {
        var entityToDelete = await _dbSet.FindAsync([id], cancellationToken);
        if (entityToDelete != null)
            Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }

        _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}