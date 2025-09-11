using MediatR;

namespace MarketWizard.Application.Interfaces.Cqrs;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}