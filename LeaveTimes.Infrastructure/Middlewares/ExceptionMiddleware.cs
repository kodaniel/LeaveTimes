using LeaveTimes.Application.Exceptions;
using LeaveTimes.Application.Services.Serializer;
using LeaveTimes.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace LeaveTimes.Infrastructure.Middlewares;

internal class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly ISerializerService _serializer;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, ISerializerService serializer)
    {
        _logger = logger;
        _serializer = serializer;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var errorResult = exception switch
            {
                MyValidationException validationException => ExceptionDetails.HandleFluentValidationException(validationException),
                ApiException apiException => ExceptionDetails.HandleApiException(apiException),
                _ => ExceptionDetails.HandleDefaultException(exception),
            };

            var errorLogLevel = exception switch
            {
                MyValidationException => LogLevel.Warning,
                ApiException => LogLevel.Warning,
                _ => LogLevel.Error
            };

            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = errorResult.Status!.Value;
                await response.WriteAsync(_serializer.Serialize(errorResult));
            }
            else
            {
                _logger.LogWarning("Can't write error response. Response has already started.");
            }
        }
    }
}
