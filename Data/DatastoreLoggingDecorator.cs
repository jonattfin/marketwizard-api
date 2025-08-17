using MarketWizardApi.ViewModels;

namespace MarketWizardApi.Data;

public class DatastoreLoggingDecorator(IDatastore datastore, ILogger<DatastoreLoggingDecorator> logger)
    : IDatastore
{
    public IEnumerable<Asset> GetWatchlistAssets(CancellationToken cancellationToken)
    {
        logger.LogInformation("Get watchlist assets from datastore");
        return datastore.GetWatchlistAssets(cancellationToken);
    }

    public IEnumerable<Portfolio> GetPortfolios(CancellationToken cancellationToken)
    {
        logger.LogInformation("Get portfolios from datastore");
        return datastore.GetPortfolios(cancellationToken);
    }

    public Portfolio? GetPortfolioById(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get portfolio id from datastore");
        return datastore.GetPortfolioById(id,  cancellationToken);
    }

    public IEnumerable<PortfolioNews> GetPortfolioNewsById(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get portfolio news from datastore");
        return datastore.GetPortfolioNewsById(id, cancellationToken);
    }

    public PortfolioPerformance? GetPortfolioPerformanceById(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get portfolio performance id from datastore");
        return datastore.GetPortfolioPerformanceById(id, cancellationToken);
    }

    public IEnumerable<PortfolioAsset> GetPortfolioAssetsById(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get portfolio assets from datastore");
        return datastore.GetPortfolioAssetsById(id, cancellationToken);
    }
}