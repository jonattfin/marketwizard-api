using MarketWizard.Domain;

namespace MarketWizard.Data.Repositories;

public interface IRepository
{
    IQueryable<Portfolio> GetPortfolios(CancellationToken cancellationToken);
    
    Task<Portfolio?> GetPortfolioById(Guid portfolioId, CancellationToken cancellationToken);
    
    Task<IQueryable<PortfolioNews>> GetPortfolioNewsById(Guid portfolioId, CancellationToken cancellationToken);

    Task<IQueryable<PortfolioAsset>> GetPortfolioAssetsById(Guid portfolioId, CancellationToken cancellationToken);
}