namespace MarketWizard.Application.Features.Stocks.GetStockBySymbol;

public class StockDto
{
  public string? Name { get; set; }
  public string? Description { get; set; }
  
  public string? ImageUrl { get; set; }
  public double? CurrentPrice { get; set; }
  public double? MarketCap { get; set; }
  public string Symbol { get; set; }
}