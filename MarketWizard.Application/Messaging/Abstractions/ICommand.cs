using MediatR;

namespace MarketWizard.Application.Messaging.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}