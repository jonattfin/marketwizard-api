using MediatR;

namespace MarketWizard.Application.Messaging.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}