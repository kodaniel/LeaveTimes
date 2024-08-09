namespace LeaveTimes.Application.Features.LeaveTimes.Search;

public sealed record LeaveTimeResponse(
    Guid Id,
    string EmployeeName,
    DateTime StartDate,
    DateTime EndDate,
    Reason Reason,
    string? Comment,
    bool IsApproved);
