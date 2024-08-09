using System.Net;

namespace LeaveTimes.Domain.Exceptions;

public class LeaveTimeAlreadyApprovedException : ApiException
{
    public LeaveTimeAlreadyApprovedException() : base($"Can not remove an approved leave time.", HttpStatusCode.BadRequest)
    {
    }
}
