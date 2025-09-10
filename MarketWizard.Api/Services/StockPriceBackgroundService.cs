using HotChocolate.Subscriptions;
using MarketWizard.Application.Interfaces.Infra;
using MarketWizard.Application.Interfaces.Persistence;

namespace MarketWizardApi.Services;

public class StockPriceBackgroundService(
    ILogger<StockPriceBackgroundService> logger,
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
            
            var stockQuotes =  await finnhubService.GetMultipleStockQuote(symbols.ToList());
            await eventSender.SendAsync("StocksPriceUpdated", stockQuotes, cancellationToken);

            await Task.Delay(TimeSpan.FromSeconds(20), cancellationToken);
        }
    }
}