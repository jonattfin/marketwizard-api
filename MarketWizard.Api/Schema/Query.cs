using Mapster;
using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Dto;
using MarketWizardApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Query
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<AssetDto>> GetWatchlistAssets([FromServices] IUnitOfWork unitOfWork,
        [FromServices] IFinnhubService finnhubService,
        Guid userId, CancellationToken cancellationToken)
    {
        var assets = (await unitOfWork.WatchlistRepository.GetAllWithPriceHistories(userId, cancellationToken)).ToList();
        var stockQuotes = await finnhubService.GetMultipleStockQuote(assets.Select(x => x.Symbol), cancellationToken);

        return assets.ToAssetDtos(stockQuotes);
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