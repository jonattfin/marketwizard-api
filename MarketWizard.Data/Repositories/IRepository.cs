using MarketWizard.Domain;
using MarketWizard.Domain.Entities;

namespace MarketWizard.Data.Repositories;

public interface IRepository
{
    Task<Guid> AddPortfolio(Portfolio portfolio, CancellationToken cancellationToken);
    
    IQueryable<Asset> GetAssets(CancellationToken cancellationToken);
    
    IQueryable<Portfolio> GetPortfolios(CancellationToken cancellationToken);
    
    Task<Portfolio?> GetPortfolioById(Guid id, CancellationToken cancellationToken);
    
}