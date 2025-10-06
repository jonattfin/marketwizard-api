using System.Text.Json;
using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Application.Features.Watchlist.GetWatchlist;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Finnhub.Services;

public class FinnhubService(HttpClient httpClient, IConfiguration configuration, ILogger<FinnhubService> logger)
    : IFinnhubService
{
    public async Task<StockQuoteDto?> GetStockQuote(string symbol, CancellationToken cancellationToken = default)
    {
        try
        {
            var finnhubSection = configuration.GetSection("Finnhub");
            
            var response =
                await httpClient.GetAsync($"quote?symbol={symbol}&token={finnhubSection.GetValue<string>("Token")}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            var stockData = JsonSerializer.Deserialize<Dictionary<string, decimal>>(jsonResponse);

            if (stockData == null)
            {
                return null;
            }

            return new StockQuoteDto
            {
                Symbol = symbol,
                CurrentPrice = stockData.GetValueOrDefault("c"),
                Change = stockData.GetValueOrDefault("d"),
                PercentChange = stockData.GetValueOrDefault("dp"),
                HighPrice = stockData.GetValueOrDefault("h"),
                LowPrice = stockData.GetValueOrDefault("l"),
                OpenPrice = stockData.GetValueOrDefault("o"),
                PreviousClosePrice = stockData.GetValueOrDefault("pc")
            };
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);;
            return null;
        }
    }

    public async Task<IReadOnlyList<StockQuoteDto>> GetMultipleStockQuote(IEnumerable<string> symbols, CancellationToken cancellationToken = default)
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