namespace LeaveTimes.Domain.Exceptions;

public class LeaveTimeNotFoundException(Guid id) : ApiException($"Leave time with id '{id}' not found.", System.Net.HttpStatusCode.NotFound)
{
}
