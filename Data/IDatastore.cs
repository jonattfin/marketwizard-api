using MarketWizardApi.ViewModels;

namespace MarketWizardApi.Data;

public interface IDatastore
{
    IEnumerable<Asset> GetWatchlistAssets(CancellationToken cancellationToken);
    
    IEnumerable<Portfolio> GetPortfolios(CancellationToken cancellationToken);
    
    Portfolio? GetPortfolioById(Guid id, CancellationToken cancellationToken);
    
    IEnumerable<PortfolioNews> GetPortfolioNewsById(Guid id, CancellationToken cancellationToken);
    
    PortfolioPerformance? GetPortfolioPerformanceById(Guid id, CancellationToken cancellationToken);
    
    IEnumerable<PortfolioAsset> GetPortfolioAssetsById(Guid id, CancellationToken cancellationToken);
}