using MarketWizard.Application.Dto;

namespace MarketWizard.Application.Contracts.Infra;

public interface IFinnhubService
{
    Task<StockQuoteDto?> GetStockQuote(string symbol, CancellationToken cancellationToken = default);
    
    Task<ICollection<StockQuoteDto>> GetMultipleStockQuote(IEnumerable<string> symbols, CancellationToken cancellationToken = default);
}