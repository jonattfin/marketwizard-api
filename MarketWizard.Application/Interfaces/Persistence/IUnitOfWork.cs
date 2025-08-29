namespace MarketWizard.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    IPortfolioRepository PortfolioRepository { get; }
    
    IAssetRepository AssetRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}