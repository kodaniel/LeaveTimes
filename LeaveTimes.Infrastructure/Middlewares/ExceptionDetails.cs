using LeaveTimes.Application.Validation;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace LeaveTimes.Infrastructure.Middlewares;

public class ExceptionDetails
{
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public Guid TraceId { get; set; } = Guid.NewGuid();
    public List<string>? Errors { get; private set; }
    public int? Status { get; set; }
    public string? StackTrace { get; set; }

    internal static ExceptionDetails HandleFluentValidationException(ValidationException exception)
    {
        var errorResult = new ExceptionDetails()
        {
            Title = "Validation Failed",
            Detail = "One or More Validations failed",
            Status = (int)HttpStatusCode.BadRequest,
            Errors = [],
        };
        if (exception.Errors.Count() == 1)
        {
            errorResult.Detail = exception.Errors.FirstOrDefault()?.ErrorMessage;
        }
        foreach (var error in exception.Errors)
        {
            errorResult.Errors.Add(error.ErrorMessage);
        }
        return errorResult;
    }

    internal static ExceptionDetails HandleDefaultException(Exception exception)
    {
        var errorResult = new ExceptionDetails()
        {
            Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError),
            Detail = exception.Message.Trim(),
            Status = (int)HttpStatusCode.InternalServerError,
        };
        return errorResult;
    }
}
