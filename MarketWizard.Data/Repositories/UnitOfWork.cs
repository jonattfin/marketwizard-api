using MarketWizard.Application.Interfaces.Persistence;

namespace MarketWizard.Data.Repositories;

public class UnitOfWork(MarketWizardContext context): IUnitOfWork
{
    public IPortfolioRepository PortfolioRepository { get; } = new PortfolioRepository(context);
    public IAssetRepository AssetRepository { get; } = new AssetRepository(context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}