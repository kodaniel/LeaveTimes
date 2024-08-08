namespace LeaveTimes.Application.Exceptions;

public class NotFoundException(object id) : ApiException($"Entity with id '{id}' not found.", System.Net.HttpStatusCode.NotFound)
{
}
