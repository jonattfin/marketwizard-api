using Mapster;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Query
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<GetAssetDto>> GetWatchlistAssets([FromServices] IUnitOfWork unitOfWork, Guid userId,
        CancellationToken cancellationToken)
    {
        var assets = await unitOfWork.WatchlistRepository.GetAllWithPriceHistories(userId, cancellationToken);
        return assets.Adapt<IEnumerable<GetAssetDto>>();
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