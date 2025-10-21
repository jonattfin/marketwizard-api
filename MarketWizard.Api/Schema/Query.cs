using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Application.Features.Portfolios.GetPortfolioById;
using MarketWizard.Application.Features.Portfolios.GetPortfolios;
using MarketWizard.Application.Features.Stocks.GetStockBySymbol;
using MarketWizard.Application.Features.Stocks.GetSwotAnalysis;
using MarketWizard.Application.Features.Watchlist.GetWatchlist;
using MarketWizard.Application.Features.Watchlist.GetWatchlists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Query
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<WatchlistDto>> GetWatchlists([FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetWatchlistsQuery(), cancellationToken);
    }
    
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<AssetDto>> GetWatchlistAssets([FromServices] IMediator mediator, Guid watchlistId,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetWatchlistQuery() {WatchlistId = watchlistId}, cancellationToken);
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting()]
    public async Task<IQueryable<PortfolioSummaryDto>> GetPortfolios([FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetPortfoliosQuery(), cancellationToken);
    }

    [UseProjection]
    public async Task<PortfolioDetailsDto?> GetPortfolioById([FromServices] IMediator mediator, Guid portfolioId,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetPortfolioByIdQuery() { PortfolioId = portfolioId }, cancellationToken);
    }

    [UseProjection]
    public async Task<StockDto?> GetStockBySymbol([FromServices] IMediator mediator, string stockSymbol,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetStockBySymbolQuery() { Symbol = stockSymbol }, cancellationToken);
    }

    [UseProjection]
    public async Task<SwotAnalysis> GetSwotAnalysis([FromServices] IMediator mediator, string companyName,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetSwotAnalysisQuery() { CompanyName = companyName }, cancellationToken);
    }
}