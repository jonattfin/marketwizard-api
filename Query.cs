using MarketWizardApi.Data;
using MarketWizardApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi;

public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<Asset> GetWatchlistAssets([FromServices] IDatastore dataStore, CancellationToken cancellationToken)
        => dataStore.GetWatchlistAssets(cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<Portfolio> GetPortfolios([FromServices] IDatastore dataStore, CancellationToken cancellationToken)
        => dataStore.GetPortfolios(cancellationToken);
    
    [UseProjection]
    public Portfolio? GetPortfolioById([FromServices] IDatastore dataStore, string id, CancellationToken cancellationToken)
        => dataStore.GetPortfolioById(id, cancellationToken);
    
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<PortfolioNews> GetPortfolioNewsById([FromServices] IDatastore dataStore, string id, CancellationToken cancellationToken)
        => dataStore.GetPortfolioNewsById(id, cancellationToken);
}