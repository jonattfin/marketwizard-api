using MarketWizard.Data.Repositories;
using MarketWizard.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi;

public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Asset> GetWatchlistAssets([FromServices] IRepository repository,
        CancellationToken cancellationToken)
        => repository.GetAssets(cancellationToken);

    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting()]
    public IQueryable<Portfolio> GetPortfolios([FromServices] IRepository repository,
        CancellationToken cancellationToken)
        => repository.GetPortfolios(cancellationToken);

    [UseProjection]
    public async Task<Portfolio?> GetPortfolioById([FromServices] IRepository repository, Guid portfolioId,
        CancellationToken cancellationToken)
        => await repository.GetPortfolioById(portfolioId, cancellationToken);
    
    
    [UseProjection]
    public async Task<PortfolioPerformance?> GetPortfolioPerformanceById([FromServices] IRepository repository, Guid portfolioId,
        CancellationToken cancellationToken)
        => await repository.GetPortfolioPerformanceById(portfolioId, cancellationToken);
    
    
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting()]
    public IEnumerable<PortfolioNews> GetPortfolioNewsById([FromServices] IRepository repository, Guid portfolioId,
        CancellationToken cancellationToken)
        => repository.GetPortfolioNewsById(portfolioId, cancellationToken);
}