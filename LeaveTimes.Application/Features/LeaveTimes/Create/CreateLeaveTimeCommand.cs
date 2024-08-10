using LeaveTimes.Application.Features.LeaveTimes.Search;

namespace LeaveTimes.Application.Features.LeaveTimes.Create;

public sealed record CreateLeaveTimeCommand(
    string? EmployeeName, 
    string? StartDate, 
    string? EndDate, 
    string? Reason, 
    string? Comment) : IRequest<LeaveTimeResponse>;
