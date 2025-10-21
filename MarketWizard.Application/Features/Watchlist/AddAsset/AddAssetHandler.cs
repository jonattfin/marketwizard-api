using MarketWizard.Application.Contracts.CQRS;
using MarketWizard.Application.Contracts.Persistence;
using MediatR;

namespace MarketWizard.Application.Features.Watchlist.AddAsset;

public record AddAssetCommand(AddAssetInputDto AddAssetInputDto) : ICommand<AddAssetOutputDto>;

public class AddAssetInputDto
{
    public Guid WatchlistId { get; set; }
    public Guid AssetId { get; set; }
}

public class AddAssetOutputDto
{
    public Guid Id { get; set; }
}

public record AddAssetCommandHandler(AddAssetInputDto AddAssetInput) : ICommand<AddAssetOutputDto>;

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

        var asset = await unitOfWork.AssetRepository.GetById(request.AddAssetInputDto.AssetId, cancellationToken);
        if (asset == null)
        {
            throw new Exception("Asset not found");       
        }

        if (watchlist.Assets.Any(x => x.Id == asset.Id)) 
            return new AddAssetOutputDto() { Id = asset.Id };
        
        watchlist.Assets.Add(asset);
        unitOfWork.WatchlistRepository.Update(watchlist);
        
        await unitOfWork.Commit(cancellationToken);
        
        return new AddAssetOutputDto() { Id = asset.Id };
    }
}