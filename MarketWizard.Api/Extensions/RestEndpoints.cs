using MarketWizard.Application.Features.Portfolios.GetPortfolioById;
using MarketWizard.Application.Features.Portfolios.GetPortfolios;
using MarketWizard.Application.Features.Watchlist.GetWatchlist;
using MediatR;

namespace MarketWizardApi.Extensions;

public static class RestEndpoints
{
  public static void AddRestEndpoints(this WebApplication app)
  {
    app.AddRootEndpoint();
    app.AddWatchlistEndpoints();
    app.AddPortfoliosEndpoints();
  }

  private static void AddRootEndpoint(this WebApplication app)
  {
    app.MapGet("/api", () => "MarketWizard API");
  }

  private static void AddWatchlistEndpoints(this WebApplication app)
  {
    app.MapGet("/api/watchlist",
      async (IMediator mediator, CancellationToken cancellationToken) =>
      {
        var assets = await mediator.Send(new GetWatchlistQuery(), cancellationToken);
        Results.Ok(assets);
      });
  }

  private static void AddPortfoliosEndpoints(this WebApplication app)
  {
    app.MapGet("/api/portfolios/{id}",
      async (Guid portfolioId, IMediator mediator, CancellationToken cancellationToken) =>
      {
        var portfolio = await mediator.Send(new GetPortfolioByIdQuery() { PortfolioId = portfolioId },
          cancellationToken);
        return portfolio != null ? Results.Ok(portfolio) : Results.NotFound();
      });

    app.MapGet("/api/portfolios",
      async (IMediator mediator, CancellationToken cancellationToken) =>
      {
        var portolios = await mediator.Send(new GetPortfoliosQuery(), cancellationToken);
        return Results.Ok(portolios);
      });
  }
}