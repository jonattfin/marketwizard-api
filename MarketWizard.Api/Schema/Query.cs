using Mapster;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Dto;
using MarketWizard.Domain.Entities;
using MarketWizardApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Query
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<AssetDto>> GetWatchlistAssets([FromServices] IUnitOfWork unitOfWork, Guid userId,
        CancellationToken cancellationToken)
    {
        var assets = await unitOfWork.WatchlistRepository.GetAllWithPriceHistories(userId, cancellationToken);

        return assets.ToAssetDtos();
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting()]
    public IQueryable<PortfolioSummaryDto> GetPortfolios([FromServices] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var portfolios = unitOfWork.PortfolioRepository.GetAllWithRelatedEntities(cancellationToken);
        
        return portfolios.ToSummaryDtos();
    }

    [UseProjection]
    public async Task<PortfolioDetailsDto?> GetPortfolioById([FromServices] IUnitOfWork unitOfWork, Guid portfolioId,
        CancellationToken cancellationToken)
    {
        var portfolio = await unitOfWork.PortfolioRepository.GetByIdWithRelatedEntities(portfolioId, cancellationToken);
        
        return portfolio?.ToDetailsDto();
    }
}