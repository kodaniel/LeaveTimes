using LeaveTimes.Application.Services.Serializer;
using LeaveTimes.Application.Validation;
using Microsoft.AspNetCore.Http;
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
                ValidationException validationException => ExceptionDetails.HandleFluentValidationException(validationException),
                _ => ExceptionDetails.HandleDefaultException(exception),
            };

            var errorLogLevel = exception switch
            {
                ValidationException => LogLevel.Warning,
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
