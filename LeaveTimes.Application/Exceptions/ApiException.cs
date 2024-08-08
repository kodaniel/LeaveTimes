using System.Net;

namespace LeaveTimes.Application.Exceptions;

public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public ApiException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public ApiException(string message) : this(message, HttpStatusCode.InternalServerError)
    {
    }
}
