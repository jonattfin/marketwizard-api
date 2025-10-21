using MarketWizard.Application.Contracts.Infra;
using MarketWizard.Application.Contracts.Persistence;
using MediatR;

namespace MarketWizard.Application.Features.Watchlist.GetWatchlists;

public class GetWatchlistsQuery : IRequest<IEnumerable<WatchlistDto>>
{
}

public class GetWatchlistsQueryHandler(IUnitOfWork unitOfWork, IUserService userService)
    : IRequestHandler<GetWatchlistsQuery, IEnumerable<WatchlistDto>>
{
    public async Task<IEnumerable<WatchlistDto>> Handle(GetWatchlistsQuery request, CancellationToken cancellationToken)
    {
        var watchlists = await unitOfWork.WatchlistRepository.GetAllForUser(
            userService.GetAuthenticatedUserId(), cancellationToken);

        return watchlists.Select(x => new WatchlistDto()
        {
            Id = x.Id,
            Name = x.Name
        });
    }
}

public class WatchlistDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}