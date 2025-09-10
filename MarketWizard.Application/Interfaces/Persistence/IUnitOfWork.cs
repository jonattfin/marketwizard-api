namespace MarketWizard.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    IPortfolioRepository PortfolioRepository { get; }
    
    IWatchlistRepository WatchlistRepository { get; }
    
    Task Commit(CancellationToken cancellationToken);
    
    Task Rollback(CancellationToken cancellationToken);
}