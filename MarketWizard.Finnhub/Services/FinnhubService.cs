using System.Text.Json;
using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Application.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Finnhub.Services;

public class FinnhubService(HttpClient httpClient, IConfiguration configuration, ILogger<FinnhubService> logger)
    : IFinnhubService
{
    private async Task<StockQuoteDto?> GetStockQuote(string symbol)
    {
        try
        {
            var finnhubSection = configuration.GetSection("Finnhub");
            
            var response =
                await httpClient.GetAsync($"quote?symbol={symbol}&token={finnhubSection.GetValue<string>("Token")}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

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

    public async Task<List<StockQuoteDto>> GetMultipleStockQuote(List<string> symbols)
    {
        var stockQuotes = new List<StockQuoteDto>();
        foreach (var symbol in symbols)
        {
            var stockQuote = await GetStockQuote(symbol);
            if (stockQuote != null)
            {
                stockQuotes.Add(stockQuote);
            }
        }

        return stockQuotes;
    }
}