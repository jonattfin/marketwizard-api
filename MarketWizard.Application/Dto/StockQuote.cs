﻿namespace MarketWizard.Application.Dto;

public class StockQuote
{
    public string Symbol { get; set; }
    public decimal? CurrentPrice { get; set; }
    public decimal? Change { get; set; }

    public decimal? PercentChange { get; set; }
    public decimal? HighPrice { get; set; }
    public decimal? LowPrice { get; set; }
    public decimal? OpenPrice { get; set; }
    public decimal? PreviousClosePrice { get; set; }
}