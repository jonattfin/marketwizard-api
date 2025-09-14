using HotChocolate.Subscriptions;
using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Finnhub.Services;

public class StockPriceBackgroundService(
    IFinnhubService finnhubService,
    ITopicEventSender eventSender,
    IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        while (!cancellationToken.IsCancellationRequested)
        {
            var watchlist = await unitOfWork.WatchlistRepository.GetAllWithAssets(cancellationToken);
            var symbols = watchlist.SelectMany(watchlistItem => watchlistItem.Assets).Distinct().Select(x => x.Symbol);

            var stockQuotes = await finnhubService.GetMultipleStockQuote(symbols.ToList(), cancellationToken);
            await eventSender.SendAsync("StocksPriceUpdated", stockQuotes, cancellationToken);

            await Task.Delay(TimeSpan.FromSeconds(60), cancellationToken);
        }
    }
}