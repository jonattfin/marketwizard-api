namespace MarketWizard.Application.Contracts.Infra;

public class SwotAnalysis
{
    public string CompanyName { get; set; } = string.Empty;
    
    public string[] Strengths { get; set; } = [];
    
    public string[] Weaknesses { get; set; } = [];
    
    public string[] Threats { get; set; }  = [];
    
    public string[] Opportunities { get; set; }  = [];

    public string Response { get; set; } = string.Empty;
}

public interface ISwotService
{
    public Task<SwotAnalysis> GetSwotAnalysis(string companyName);
}