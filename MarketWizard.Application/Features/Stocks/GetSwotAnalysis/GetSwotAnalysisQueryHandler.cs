using MarketWizard.Application.Contracts.Infra;
using MediatR;

namespace MarketWizard.Application.Features.Stocks.GetSwotAnalysis;

public class GetSwotAnalysisQuery : IRequest<SwotAnalysis>
{
  public string CompanyName { get; set; }
}

public class GetSwotAnalysisQueryHandler(ISwotService swotService) : IRequestHandler<GetSwotAnalysisQuery, SwotAnalysis>
{
  public async Task<SwotAnalysis> Handle(GetSwotAnalysisQuery request, CancellationToken cancellationToken)
    => await swotService.GetSwotAnalysis(request.CompanyName);
}