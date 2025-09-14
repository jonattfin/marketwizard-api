using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketWizard.Application.Behaviours;

public class ExceptionHandlingBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Request {Request} failed with exception {Message}", typeof(TRequest).Name, ex.Message);

            // Optionally, you can wrap or translate the exception here before rethrowing
            throw;
        }
    }
}