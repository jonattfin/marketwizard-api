using MarketWizardApi.Data;
using MarketWizardApi.ViewModels;

namespace MarketWizardApi;

public class Query(IDatastore datastore)
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<Asset> GetWatchlistAssets(CancellationToken cancellationToken)
        => datastore.GetWatchlistAssets(cancellationToken);

    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<Portfolio> GetPortfolios(CancellationToken cancellationToken)
        => datastore.GetPortfolios(cancellationToken);
    
    [UseProjection]
    public Portfolio? GetPortfolioById(Guid id, CancellationToken cancellationToken)
        => datastore.GetPortfolioById(id, cancellationToken);
    
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<PortfolioNews> GetPortfolioNewsById(Guid id, CancellationToken cancellationToken)
        => datastore.GetPortfolioNewsById(id, cancellationToken);
    
    [UseProjection]
    public PortfolioPerformance? GetPortfolioPerformanceById(Guid id, CancellationToken cancellationToken)
        => datastore.GetPortfolioPerformanceById(id, cancellationToken);
    
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<PortfolioAsset> GetPortfolioAssetsById(Guid id, CancellationToken cancellationToken)
        => datastore.GetPortfolioAssetsById(id, cancellationToken);
}