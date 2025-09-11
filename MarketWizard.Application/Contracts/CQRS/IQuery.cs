using MediatR;

namespace MarketWizard.Application.Contracts.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}