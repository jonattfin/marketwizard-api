using HotChocolate.Subscriptions;
using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarketWizard.Finnhub.Services;

public class StockPriceBackgroundService(
    IFinnhubService finnhubService,
    ITopicEventSender eventSender,
    IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var watchlist = await unitOfWork.WatchlistRepository.GetAllWithAssets(stoppingToken);
            var symbols = watchlist.SelectMany(watchlistItem => watchlistItem.Assets).Distinct().Select(x => x.Symbol);

            var stockQuotes = await finnhubService.GetMultipleStockQuote(symbols.ToList(), stoppingToken);
            await eventSender.SendAsync("StocksPriceUpdated", stockQuotes, stoppingToken);

            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
        }
    }
}