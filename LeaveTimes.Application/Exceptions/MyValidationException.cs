namespace LeaveTimes.Application.Exceptions;

public class MyValidationException : ApiException
{
    public IEnumerable<ValidationFailure> Errors { get; private set; }

    public MyValidationException(string message) : this(message, Enumerable.Empty<ValidationFailure>())
    {
    }

    public MyValidationException(string message, IEnumerable<ValidationFailure> errors) : base(message, System.Net.HttpStatusCode.BadRequest)
    {
        Errors = errors;
    }
}
