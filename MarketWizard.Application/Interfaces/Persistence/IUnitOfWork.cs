namespace MarketWizard.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    IPortfolioRepository PortfolioRepository { get; }
    
    IWatchlistRepository WatchlistRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}