using MarketWizard.Application.Interfaces;
using MarketWizard.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Asset> GetWatchlistAssets([FromServices] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => unitOfWork.AssetRepository.Get(cancellationToken);

    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting()]
    public IQueryable<Portfolio> GetPortfolios([FromServices] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => unitOfWork.PortfolioRepository.Get(cancellationToken);

    [UseProjection]
    public async Task<Portfolio?> GetPortfolioById([FromServices] IUnitOfWork unitOfWork, Guid portfolioId,
        CancellationToken cancellationToken)
        => await unitOfWork.PortfolioRepository.GetById(portfolioId, cancellationToken);

}