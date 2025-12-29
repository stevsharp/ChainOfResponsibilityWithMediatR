using MediatR;

using Microsoft.Extensions.Logging;

using System.Diagnostics;

namespace ChainOfResponsibility.Application.Common.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        var name = typeof(TRequest).Name;
        var sw = Stopwatch.StartNew();

        logger.LogInformation("Handling {Request}", name);

        try
        {
            var response = await next(ct);
            logger.LogInformation("Handled {Request} in {Ms} ms", name, sw.ElapsedMilliseconds);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed {Request} after {Ms} ms", name, sw.ElapsedMilliseconds);
            throw;
        }
    }
}
