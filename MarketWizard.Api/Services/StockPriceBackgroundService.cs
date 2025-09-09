using System.Text.Json;
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
            foreach (var asset in watchlist.SelectMany(watchlistItem => watchlistItem.Assets).Distinct())
            {
                try
                {
                    var stockQuote = await finnhubService.GetStockQuote(asset.Symbol);

                    if (stockQuote == null)
                    {
                        continue;
                    }

                    logger.LogInformation("Sending stock price update for {stockQuote}", 
                        JsonSerializer.Serialize(stockQuote, new JsonSerializerOptions { WriteIndented = true }));
                    
                    await eventSender.SendAsync("StockPriceUpdated", stockQuote, cancellationToken);
                }
                catch (Exception e)
                {
                    logger.LogError(e, e.Message);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        }
    }
}