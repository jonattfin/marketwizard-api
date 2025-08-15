using MarketWizardApi.ViewModels;

namespace MarketWizardApi.Data;

public interface IDatastore
{
    IEnumerable<Asset> GetWatchlistAssets(CancellationToken cancellationToken);
    
    IEnumerable<Portfolio> GetPortfolios(CancellationToken cancellationToken);
    
    Portfolio? GetPortfolioById(string id, CancellationToken cancellationToken);
    
    IEnumerable<PortfolioNews> GetPortfolioNewsById(string id, CancellationToken cancellationToken);
}