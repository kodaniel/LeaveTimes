namespace LeaveTimes.Application.Validation;

public class ValidationException : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; private set; }

    public ValidationException(string message) : this(message, Enumerable.Empty<ValidationFailure>())
    {
    }

    public ValidationException(string message, IEnumerable<ValidationFailure> errors) : base(message)
    {
        Errors = errors;
    }
}
