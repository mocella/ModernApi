using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Api.Core.Middleware;

public class HttpClientLoggingHandler : DelegatingHandler
{
    private readonly ILogger _logger;

    public HttpClientLoggingHandler(ILogger logger)
    { 
        _logger = logger;
        InnerHandler = new HttpClientHandler();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var requestLog = new StringBuilder();

        requestLog.AppendLine($"{Environment.NewLine}REQUEST Uri: {request.RequestUri} - HttpMethod: {request.Method}");
        try
        {
            var content = request.Content;
            var jsonContent = await content!.ReadAsStringAsync();
            // note: may need a strategy to filter sensitive info from these payloads
            requestLog.AppendLine($"REQUEST Body : {jsonContent}");
        }
        catch (Exception)
        {
            requestLog.AppendLine($"REQUEST Body : N/A");
        }

        var response = await base.SendAsync(request, cancellationToken);

        requestLog.AppendLine($"RESPONSE Status: {response.StatusCode}");
        try
        {
            var content = response.Content;
            var jsonContent = await content!.ReadAsStringAsync();
            // note: may need a strategy to filter sensitive info from these payloads
            requestLog.AppendLine($"RESPONSE Body : {jsonContent}");
        }
        catch (Exception)
        {
            requestLog.AppendLine($"RESPONSE Body : N/A");
        }

        _logger.Log(LogLevel.Information, requestLog.ToString(), "HttpClientLoggingHandler");

        return response;
    }
}