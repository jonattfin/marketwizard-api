using MarketWizard.Domain.Entities;

namespace MarketWizard.Application.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<Portfolio> PortfolioRepository { get; }
    
    IGenericRepository<Asset> AssetRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}