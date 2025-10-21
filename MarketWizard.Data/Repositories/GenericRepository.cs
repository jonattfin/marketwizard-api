using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class GenericRepository<TEntity>(MarketWizardContext context)
    : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public virtual IQueryable<TEntity> Get(CancellationToken cancellationToken = default)
    {
        return DbSet;
    }

    public virtual async Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public virtual async Task Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public virtual async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var entityToDelete = await GetById(id, cancellationToken);
        if (entityToDelete != null)
            Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            DbSet.Attach(entityToDelete);
        }

        DbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        DbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}