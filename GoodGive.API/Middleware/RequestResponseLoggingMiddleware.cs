using System.Text;

namespace GoodGive.API.Middleware;

public class RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);
        }
        finally
        {
            await LogRequest(context.Request);
        }

        await LogResponse(context.Response, $"{context.Request?.Method} {context.Request?.Scheme}://{context.Request?.Host}{context.Request?.Path.Value}{context.Request?.QueryString.Value}");

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private async Task LogRequest(HttpRequest request)
    {
        var requestHeaders = new StringBuilder();
        foreach (var header in request.Headers)
        {
            requestHeaders.AppendLine($"{header.Key}: {header.Value}");
        }

        string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

        string requestLogBuilder = $"\nREQUEST -> {request?.Method} {request?.Scheme}://{request?.Host}{request?.Path.Value}{request?.QueryString.Value}"
                + $"\nContentType -> {request?.ContentType}"
                + $"\nHeaders -> {requestHeaders}"
                + $"Body -> {requestBody}";

        _logger.LogInformation(requestLogBuilder);
    }

    private async Task LogResponse(HttpResponse response, string requestMethodUrl)
    {

        var responseHeaders = new StringBuilder();
        foreach (var header in response.Headers)
        {
            responseHeaders.AppendLine($"{header.Key}: {header.Value}");
        }

        response.Body.Seek(0, SeekOrigin.Begin);
        string responseBody = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        string responseLogBuilder = $"\nRESPONSE -> {requestMethodUrl}"
                + $"\nStatusCode -> {response?.StatusCode}"
                + $"\nContentType -> {response?.ContentType}"
                + $"\nHeaders -> {responseHeaders}"
                + $"Response Body -> {responseBody}";

        _logger.LogInformation(responseLogBuilder);
    }
}