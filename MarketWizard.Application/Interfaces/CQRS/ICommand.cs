using MediatR;

namespace MarketWizard.Application.Interfaces.Cqrs;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}