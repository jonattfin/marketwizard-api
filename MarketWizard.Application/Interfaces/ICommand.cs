using MediatR;

namespace MarketWizard.Application.Interfaces;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}