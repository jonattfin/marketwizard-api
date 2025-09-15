using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Dto;
using MarketWizard.Domain.Entities;
using MediatR;

namespace MarketWizard.Application.Features.Portfolios.GetPortfolios;

public class GetPortfoliosQuery : IRequest<IQueryable<PortfolioSummaryDto>>
{
}

public class GetPortfoliosQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPortfoliosQuery, IQueryable<PortfolioSummaryDto>>
{
    public Task<IQueryable<PortfolioSummaryDto>> Handle(GetPortfoliosQuery request, CancellationToken cancellationToken)
    {
        var portfolios = unitOfWork.PortfolioRepository.GetAllWithRelatedEntities(cancellationToken);

        return Task.FromResult(ToSummaryDtos(portfolios));
    }
    
    private static IQueryable<PortfolioSummaryDto> ToSummaryDtos(IQueryable<Portfolio> portfolios)
    {
        return portfolios.Select(portfolio => new PortfolioSummaryDto()
        {
            Id = portfolio.Id,
            Name = portfolio.Name,
            Description = portfolio.Description,
            ImageUrl = portfolio.ImageUrl,
            Holdings = portfolio.PortfolioAssets.Count,
            TotalValue = portfolio.TotalValue,
            UnrealizedGain = portfolio.UnrealizedGain,
            CreatedAt = portfolio.CreatedAt,
        });
    }
}