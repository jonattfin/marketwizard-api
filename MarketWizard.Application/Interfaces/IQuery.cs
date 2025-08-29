using MediatR;

namespace MarketWizard.Application.Interfaces;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}