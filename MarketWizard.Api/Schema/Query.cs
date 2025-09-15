using MarketWizard.Application.Features.Portfolios.GetPortfolioById;
using MarketWizard.Application.Features.Portfolios.GetPortfolios;
using MarketWizard.Application.Features.Watchlist.GetWatchlist;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Query
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<AssetDto>> GetWatchlistAssets([FromServices] IMediator mediator,
        Guid userId, CancellationToken cancellationToken)
    {
         return await mediator.Send(new GetWatchlistQuery() {UserId = userId}, cancellationToken);
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting()]
    public async Task<IQueryable<PortfolioSummaryDto>> GetPortfolios( [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetPortfoliosQuery(), cancellationToken);
    }

    [UseProjection]
    public async Task<PortfolioDetailsDto?> GetPortfolioById([FromServices] IMediator mediator, Guid portfolioId,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetPortfolioByIdQuery() {PortfolioId = portfolioId}, cancellationToken);
    }
}