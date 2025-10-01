using MarketWizard.Application.Features.Portfolios.GetPortfolioById;
using MarketWizard.Application.Features.Portfolios.GetPortfolios;
using MediatR;

namespace MarketWizardApi.Extensions;

public static class PortfoliosEndpoints
{
  public static void AddPortfoliosEndpoints(this WebApplication app)
  {
    app.MapGet("/api/portfolios/{id}",
      async (Guid portfolioId, IMediator mediator, CancellationToken cancellationToken) =>
      {
        var portfolio = await mediator.Send(new GetPortfolioByIdQuery() { PortfolioId = portfolioId },
          cancellationToken);
        return portfolio != null ? Results.Ok(portfolio) : Results.NotFound();
      });

    app.MapGet("/portfolios",
      async (IMediator mediator, CancellationToken cancellationToken) =>
      {
        var portolios = await mediator.Send(mediator.Send(new GetPortfoliosQuery(), cancellationToken));
        return Results.Ok(portolios);
      });
  }
}