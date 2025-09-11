using MarketWizard.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace MarketWizard.Data.Repositories;

public class UnitOfWork: IUnitOfWork, IDisposable
{
    private readonly MarketWizardContext _context;
    private readonly IDbContextTransaction _transaction;
    
    private bool _disposed;
    
    public UnitOfWork(MarketWizardContext context)
    {
        _context = context;
        _transaction = _context.Database.BeginTransaction();
        
        PortfolioRepository = new PortfolioRepository(_context);
        WatchlistRepository = new WatchlistRepository(_context);
        UserRepository = new UserRepository(_context);
    }
    
    public IPortfolioRepository PortfolioRepository { get; }
    
    public IWatchlistRepository WatchlistRepository { get; }
    
    public IUserRepository UserRepository { get; }

    public async Task Commit(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await Rollback(cancellationToken);
            throw;
        }
    }

    public async Task Rollback(CancellationToken cancellationToken)
    {
        await _transaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        if (_disposed) return;
        
        _transaction.Dispose();
        _context.Dispose();
        _disposed = true;
    }
}