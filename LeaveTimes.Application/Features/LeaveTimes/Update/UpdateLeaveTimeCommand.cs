using LeaveTimes.Application.Features.LeaveTimes.Search;

namespace LeaveTimes.Application.Features.LeaveTimes.Update;

public sealed record UpdateLeaveTimeCommand : IRequest<LeaveTimeResponse>
{
    public sealed record UpdateLeaveTimeCommandBody(
        string EmployeeName,
        DateTime StartDate,
        DateTime EndDate,
        string? Reason,
        string? Comment);

    public Guid Id { get; init; }

    public UpdateLeaveTimeCommandBody Item { get; init; }

    public UpdateLeaveTimeCommand(Guid id, UpdateLeaveTimeCommandBody item)
    {
        Id = id;
        Item = item;
    }
}
