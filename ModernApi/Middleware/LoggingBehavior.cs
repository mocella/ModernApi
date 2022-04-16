namespace ModernApi.Middleware;

using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using MediatR;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        //Request
        _logger.LogInformation($"Handling {typeof(TRequest).Name} With Request: {JsonSerializer.Serialize(request)}");

        var stopwatch = new Stopwatch();

        stopwatch.Restart();
        var response = await next();
        stopwatch.Stop();

        //Response
        _logger.LogInformation($"Handled {typeof(TResponse).Name} in {stopwatch.ElapsedMilliseconds}ms");
        return response;
    }
}