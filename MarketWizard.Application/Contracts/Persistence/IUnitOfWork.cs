namespace MarketWizard.Application.Contracts.Persistence;

public interface IUnitOfWork
{
    IPortfolioRepository PortfolioRepository { get; }
    
    IWatchlistRepository WatchlistRepository { get; }
    
    IUserRepository UserRepository { get; }
    
    Task Commit(CancellationToken cancellationToken);
    
    Task Rollback(CancellationToken cancellationToken);
}