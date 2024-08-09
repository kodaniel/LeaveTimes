namespace LeaveTimes.Application.Features.LeaveTimes.Delete;

public sealed record DeleteLeaveTimeCommand(Guid Id) : IRequest;
