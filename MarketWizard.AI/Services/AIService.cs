using MarketWizard.Application.Contracts.Infra;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace MarketWizard.AI.Services;

public class SwotService : ISwotService
{
    private readonly ChatClient _chatClient;
    
    public SwotService(IConfiguration configuration)
    {
        var aiSection = configuration.GetSection("AIApi");
        _chatClient = new ChatClient("gpt-4o", aiSection.GetValue<string>("Token"));
    }
    
    public async Task<SwotAnalysis> GetSwotAnalysis(string companyName)
    {
        ChatCompletion completion = await _chatClient.CompleteChatAsync("Create a SWOT analysis for " + companyName + ".");
        
        var swotAnalysis = new SwotAnalysis() { CompanyName = companyName, Response = completion.Content[0].Text};
        return await Task.FromResult(swotAnalysis);
    }
}