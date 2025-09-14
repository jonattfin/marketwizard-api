
using MarketWizard.Domain.Entities;

namespace MarketWizard.Application.Dto;

public class PortfolioSummaryDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; } 
    
    public string ImageUrl { get; set; }
    
    public double? TotalValue { get; set; }
    
    public double? UnrealizedGain { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public int? Holdings { get; set; }
}