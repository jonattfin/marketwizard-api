using MarketWizard.Application.Contracts.Persistence;
using MassTransit;

namespace MarketWizard.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
  private readonly MarketWizardContext _context;

  private readonly Lazy<IPortfolioRepository> _portfolioRepository;
  private readonly Lazy<IWatchlistRepository> _watchlistRepository;
  private readonly Lazy<IUserRepository> _userRepository;
  private readonly Lazy<IAssetRepository> _assetRepository;

  public UnitOfWork(MarketWizardContext context, IPublishEndpoint publishEndpoint)
  {
    _context = context;

    _portfolioRepository = new Lazy<IPortfolioRepository>(() => new PortfolioRepository(_context, publishEndpoint));
    _watchlistRepository = new Lazy<IWatchlistRepository>(() => new WatchlistRepository(_context));
    _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
    _assetRepository = new Lazy<IAssetRepository>(() => new AssetRepository(_context));
  }

  public IPortfolioRepository PortfolioRepository => _portfolioRepository.Value;

  public IWatchlistRepository WatchlistRepository => _watchlistRepository.Value;

  public IUserRepository UserRepository => _userRepository.Value;
  
  public IAssetRepository AssetRepository => _assetRepository.Value;

  public async Task Commit(CancellationToken cancellationToken)
  {
    await _context.SaveChangesAsync(cancellationToken);
  }
}