using GoodGive.BLL.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GoodGive.API.Helpers.ExceptionHelper;

public class ExceptionResultHandler<T>(ILogger logger, Exception exception)
{
    private readonly ILogger _logger = logger;
    public Guid UserId { get; set; }
    public Exception Exception { get; set; } = exception;
    public List<string> CallStack { get; set; } = [exception.GetExceptionCallStack()];

    public static ExceptionResultHandler<T> ExceptionResult(ILogger logger, Exception exception)
    {
        return new ExceptionResultHandler<T>(logger, exception);
    }

    public IActionResult HandleExceptionResult(Guid userId)
    {
        Guid LogId = Guid.NewGuid();
        UserId = userId;

        LogException(LogId);

        return new ObjectResult(new ProblemDetails()
        {
            Detail = "An exception has been thrown. Please see log for details.",
            Status = StatusCodes.Status500InternalServerError,
            Title = "Exception Error",
        });
    }

    private void LogException(Guid logId)
    {
        var resultLog = new ErrorLog
        {
            LogId = logId,
            UserId = UserId,
            Message = Exception.Message,
            MessageCallStack = CallStack,
            Exception = (Exception != null ? ExceptionResultHandler<T>.GetFullStackTraceException(Exception) : null)
        };

        _logger.LogError("An exception occurred: {ResultLog}", JsonConvert.SerializeObject(resultLog));
    }

    private static string GetFullStackTraceException(Exception exception)
    {
        StringBuilder result = new();
        result.AppendLine(exception.Message);
        result.AppendLine(exception.StackTrace);

        if (exception.InnerException != null)
        {
            result.AppendLine();
            result.AppendLine("Inner Exception:");
            result.AppendLine(ExceptionResultHandler<T>.GetFullStackTraceException(exception.InnerException));
        }

        return result.ToString();
    }
}
