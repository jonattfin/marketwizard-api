using MarketWizard.Domain;

namespace MarketWizard.Data.Repositories;

public interface IRepository
{
    Task<Guid> AddPortfolio(Portfolio portfolio);
    
    IQueryable<Asset> GetAssets(CancellationToken cancellationToken);
    
    IQueryable<Portfolio> GetPortfolios(CancellationToken cancellationToken);
    
    Task<Portfolio?> GetPortfolioById(Guid id, CancellationToken cancellationToken);
    
}