using MarketWizard.Domain.Entities;

namespace MarketWizard.Application.Dto;

public class GetAssetDto
{
    public Guid Id { get; set; }
    
    public string Symbol { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public AssetType Type { get; set; }
    
    public double? LastPrice { get; set; }
}