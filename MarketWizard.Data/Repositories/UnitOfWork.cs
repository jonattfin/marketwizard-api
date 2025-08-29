using MarketWizard.Application.Interfaces;
using MarketWizard.Domain.Entities;

namespace MarketWizard.Data.Repositories;

public class UnitOfWork(MarketWizardContext context): IUnitOfWork
{
    public IGenericRepository<Portfolio> PortfolioRepository { get; } = new GenericRepository<Portfolio>(context);
    public IGenericRepository<Asset> AssetRepository { get; } = new GenericRepository<Asset>(context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}