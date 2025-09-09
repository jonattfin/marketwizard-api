using System.Text.Json;
using MarketWizard.Application.Dto;
using MarketWizard.Application.Interfaces.Infra;
using Microsoft.Extensions.Configuration;

namespace MarketWizard.Finnhub;

public class FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    : IFinnhubService
{
    public async Task<StockQuote?> GetStockQuote(string symbol)
    {
        var finnhubSection = configuration.GetSection("Finnhub");
        
        using var client = httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(finnhubSection.GetValue<string>("BaseUrl"));;

        var response = await client.GetAsync($"quote?symbol={symbol}&token={finnhubSection.GetValue<string>("Token")}");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var stockData = JsonSerializer.Deserialize<Dictionary<string, decimal>>(jsonResponse);

        if (stockData == null)
        {
            return null;
        }

        return new StockQuote
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
}