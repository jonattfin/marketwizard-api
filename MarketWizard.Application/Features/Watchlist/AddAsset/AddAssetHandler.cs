using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;

using MarketWizard.Domain.Entities;
using MediatR;

namespace MarketWizard.Application.Features.Watchlist.AddAsset;

public record AddAssetCommand(AddAssetInputDto AddAssetInputDto) : ICommand<AddAssetOutputDto>;

public class AddAssetInputDto
{
    public Guid WatchlistId { get; set; }
    public string Symbol { get; set; }
}

public class AddAssetOutputDto
{
    public Guid Id { get; set; }
}

public record AddAssetCommandHandler(AddAssetInputDto AddPortfolio) : ICommand<AddAssetOutputDto>;

public class AddAssetHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddAssetCommand, AddAssetOutputDto>
{
    public async Task<AddAssetOutputDto> Handle(AddAssetCommand request, CancellationToken cancellationToken)
    {
        var watchlist =
            await unitOfWork.WatchlistRepository.GetByIdWithAssets(request.AddAssetInputDto.WatchlistId, cancellationToken);
        if (watchlist == null)
        {
            throw new Exception("Watchlist not found");
        }

        var asset = new Asset()
        {
            Symbol = request.AddAssetInputDto.Symbol,
            Name = request.AddAssetInputDto.Symbol,
            Description = string.Empty,
            Type = AssetType.Stock,
            Id = Guid.NewGuid()
        };

        await unitOfWork.AssetRepository.Insert(asset, cancellationToken);
        
        watchlist.Assets.Add(asset);
        unitOfWork.WatchlistRepository.Update(watchlist);
        
        await unitOfWork.Commit(cancellationToken);

        return new AddAssetOutputDto() { Id = asset.Id };
    }
}