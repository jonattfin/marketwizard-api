using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Application.Features.Watchlist.GetWatchlist;

public class GetWatchlistQuery : IRequest<IEnumerable<AssetDto>>
{
}

public class GetWatchlistQueryHandler(
  IUnitOfWork unitOfWork,
  IFinnhubService finnhubService,
  IUserService userService,
  ILogger<GetWatchlistQueryHandler> logger)
  : IRequestHandler<GetWatchlistQuery, IEnumerable<AssetDto>>
{
  public async Task<IEnumerable<AssetDto>> Handle(GetWatchlistQuery request, CancellationToken cancellationToken)
  {
    var assets = (await unitOfWork.WatchlistRepository.GetAllWithPriceHistories(cancellationToken)).ToList();
    var stockQuotes =
      await finnhubService.GetMultipleStockQuote(assets.Select(x => x.Symbol), cancellationToken);

    return ToAssetDtos(assets, stockQuotes);
  }

  private static IEnumerable<AssetDto> ToAssetDtos(IEnumerable<Asset> assets, IEnumerable<StockQuoteDto> quotes)
  {
    return assets.Select(asset => new AssetDto()
    {
      Id = asset.Id,
      Description = asset.Description,
      LastPrice = asset.LastPrice,
      Name = asset.Name,
      Symbol = asset.Symbol,
      Type = asset.Type,
      Quote = quotes.FirstOrDefault(x => x.Symbol == asset.Symbol)
    });
  }
}