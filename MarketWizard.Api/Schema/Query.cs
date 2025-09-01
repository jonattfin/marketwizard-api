using Mapster;
using MarketWizard.Application.Dto;
using MarketWizard.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<GetAssetDto> GetWatchlistAssets([FromServices] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var assets = unitOfWork.AssetRepository.GetAllWithPriceHistories(cancellationToken);
        return assets.ProjectToType<GetAssetDto>();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting()]
    public IQueryable<GetPortfolioDto> GetPortfolios([FromServices] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var portfolios = unitOfWork.PortfolioRepository.GetAllWithRelatedEntities(cancellationToken);
        return portfolios.ProjectToType<GetPortfolioDto>();
    }

    [UseProjection]
    public async Task<GetPortfolioDto?> GetPortfolioById([FromServices] IUnitOfWork unitOfWork, Guid portfolioId,
        CancellationToken cancellationToken)
    {
        var portfolio =  await unitOfWork.PortfolioRepository.GetByIdWithRelatedEntities(portfolioId, cancellationToken);
        return portfolio?.Adapt<GetPortfolioDto>();
    }
}