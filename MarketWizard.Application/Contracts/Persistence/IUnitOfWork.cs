namespace MarketWizard.Application.Contracts.Persistence;

public interface IUnitOfWork
{
    IPortfolioRepository PortfolioRepository { get; }
    
    IWatchlistRepository WatchlistRepository { get; }
    
    IUserRepository UserRepository { get; }
    
    IAssetRepository AssetRepository { get; }
    
    Task Commit(CancellationToken cancellationToken);
}