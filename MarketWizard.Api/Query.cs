using MarketWizard.Data;
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
    public IEnumerable<Asset> GetWatchlistAssets([FromServices] MarketWizardContext context,
        CancellationToken cancellationToken)
        => context.Assets;

    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting()]
    public IQueryable<Portfolio> GetPortfolios([FromServices] IRepository repository,
        CancellationToken cancellationToken)
        => repository.GetPortfolios(cancellationToken);

    [UseProjection]
    public async Task<Portfolio?> GetPortfolioById([FromServices] IRepository repository, Guid id,
        CancellationToken cancellationToken)
        => await repository.GetPortfolioById(id, cancellationToken);

    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<PortfolioNews>> GetPortfolioNewsById([FromServices] IRepository repository,
        Guid portfolioId,
        CancellationToken cancellationToken)
        => await repository.GetPortfolioNewsById(portfolioId, cancellationToken);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<PortfolioAsset>> GetPortfolioAssetsById([FromServices] IRepository repository,
        Guid portfolioId, CancellationToken cancellationToken)
        => await repository.GetPortfolioAssetsById(portfolioId, cancellationToken);
    
}