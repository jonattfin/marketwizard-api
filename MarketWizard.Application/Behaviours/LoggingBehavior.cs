using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MarketWizard.Application.Behaviours;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        logger.LogInformation("Handling {RequestName} with content: {@Request}", requestName, request);

        var stopwatch = Stopwatch.StartNew();

        var response = await next(cancellationToken);

        stopwatch.Stop();

        logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms with response: {@Response}", requestName, stopwatch.ElapsedMilliseconds, response);

        return response;
    }
}
