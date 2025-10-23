using System.Text.Json;
using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Application.Features.Watchlist.GetWatchlist;
using Microsoft.Extensions.Caching.Distributed;

namespace MarketWizard.Finnhub.Services;

public class FinnhubServiceCacheDecorator(IFinnhubService finnhubService, IDistributedCache cache) : IFinnhubService
{
    private readonly DistributedCacheEntryOptions _options = new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromDays(1));

    public async Task<StockQuoteDto?> GetStockQuote(string symbol, CancellationToken cancellationToken = default)
    {
        var cachedKey = symbol;
        var cached = await cache.GetStringAsync(cachedKey, cancellationToken);

        if (!string.IsNullOrEmpty(cached))
        {
            return JsonSerializer.Deserialize<StockQuoteDto>(cached);
        }
        
        var stockQuote = await finnhubService.GetStockQuote(symbol, cancellationToken);
        if (stockQuote != null)
        {
            await cache.SetStringAsync(cachedKey, JsonSerializer.Serialize(stockQuote), _options, cancellationToken);
        }
        return stockQuote;
    }

    public async Task<ICollection<StockQuoteDto>> GetMultipleStockQuote(IEnumerable<string> symbols, CancellationToken cancellationToken = default)
    {
        var stockQuotes = new List<StockQuoteDto>();
        foreach (var symbol in symbols)
        {
            var stockQuote = await GetStockQuote(symbol, cancellationToken);
            if (stockQuote != null)
            {
                stockQuotes.Add(stockQuote);
            }
        }
        return stockQuotes;
    }
}