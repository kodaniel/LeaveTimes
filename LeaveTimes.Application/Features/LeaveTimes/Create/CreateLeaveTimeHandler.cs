﻿using LeaveTimes.Application.Features.LeaveTimes.Search;

namespace LeaveTimes.Application.Features.LeaveTimes.Create;

public sealed class CreateLeaveTimeHandler(ILeaveTimeRepository repository) : IRequestHandler<CreateLeaveTimeCommand, LeaveTimeResponse>
{
    private readonly ILeaveTimeRepository _repository = repository;

    public async Task<LeaveTimeResponse> Handle(CreateLeaveTimeCommand request, CancellationToken cancellationToken)
    {
        var reason = Enum.Parse<Reason>(request.Reason!);
        var startDate = DateTime.Parse(request.StartDate!);
        var endDate = DateTime.Parse(request.EndDate!);

        var leaveTime = LeaveTime.Create(request.EmployeeName!, reason, startDate, endDate, request.Comment);

        await _repository.AddAsync(leaveTime, cancellationToken);

        return leaveTime.MapToDto();
    }
}
