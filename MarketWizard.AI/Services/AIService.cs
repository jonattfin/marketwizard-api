using MarketWizard.Application.Contracts.Infra;

namespace MarketWizard.AI.Services;

public class SwotService : ISwotService
{
    // private readonly ChatClient _chatClient;
    
    // public SwotService()
    // {
    //     var aiSection = configuration.GetSection("AIApi");
    //     _chatClient = new ChatClient("gpt-4o", aiSection.GetValue<string>("Token"));
    // }
    
    public async Task<SwotAnalysis> GetSwotAnalysis(string companyName)
    {
        // ChatCompletion completion = await _chatClient.CompleteChatAsync("Create a SWOT analysis for " + companyName + ".");
        // TODO
        
        var swotAnalysis = new SwotAnalysis()
        {
            CompanyName = companyName, 
            // Response = completion.Content[0].Text,
            Response = string.Empty,
            Strengths = [
                "Brand loyalty and reputation",
                "Easy-to-use software products",
                "Strong distribution channels"
            ],
            Weaknesses = [
                "Dependence on hardware manufacturers",
                "Past poor acquisitions",
                "Security flaws criticism"
            ],
            Opportunities = [
                "Cloud infrastructure growth",
                "AI innovation and new products",
                "Growth in emerging markets"
            ],
            Threats = [
                "Intense competition (AWS, Google)",
                "Rapid technological changes",
                "Cybersecurity risks"
            ]
        };
        return await Task.FromResult(swotAnalysis);
    }
}