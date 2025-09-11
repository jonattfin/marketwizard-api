using MediatR;

namespace MarketWizard.Application.Contracts.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}