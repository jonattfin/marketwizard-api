using MarketWizard.Domain.Entities;

namespace MarketWizard.Data.Repositories;

public interface IUnitOfWork
{
    IGenericRepository<Portfolio> PortfolioRepository { get; }
    
    IGenericRepository<Asset> AssetRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

public class UnitOfWork(MarketWizardContext context, IGenericRepository<Portfolio> portfolioRepository, IGenericRepository<Asset> assetRepository): IUnitOfWork
{
    public IGenericRepository<Portfolio> PortfolioRepository { get; } = portfolioRepository;
    public IGenericRepository<Asset> AssetRepository { get; } = assetRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}