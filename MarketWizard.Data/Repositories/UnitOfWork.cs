using MarketWizard.Application.Contracts.Persistence;

namespace MarketWizard.Data.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly MarketWizardContext _context;
    
    private readonly Lazy<IPortfolioRepository> _portfolioRepository;
    private readonly Lazy<IWatchlistRepository> _watchlistRepository;
    private readonly Lazy<IUserRepository> _userRepository;
    
    public UnitOfWork(MarketWizardContext context)
    {
        _context = context;
        
         _portfolioRepository = new Lazy<IPortfolioRepository>(() => new PortfolioRepository(_context));
        _watchlistRepository = new Lazy<IWatchlistRepository>(() => new WatchlistRepository(_context));
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
    }
    
    public IPortfolioRepository PortfolioRepository => _portfolioRepository.Value;
    
    public IWatchlistRepository WatchlistRepository => _watchlistRepository.Value;
    
    public IUserRepository UserRepository => _userRepository.Value;

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}