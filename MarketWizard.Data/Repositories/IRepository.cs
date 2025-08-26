using MarketWizard.Domain;

namespace MarketWizard.Data.Repositories;

public interface IRepository
{
    IQueryable<Asset> GetAssets(CancellationToken cancellationToken);
    
    IQueryable<Portfolio> GetPortfolios(CancellationToken cancellationToken);
    
    Task<Portfolio?> GetPortfolioById(Guid id, CancellationToken cancellationToken);
}