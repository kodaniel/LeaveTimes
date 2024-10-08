﻿using LeaveTimes.Application.Features.LeaveTimes.Search;

namespace LeaveTimes.Application.Features.LeaveTimes.Update;

public sealed class UpdateLeaveTimeHandler(ILeaveTimeRepository repository) : IRequestHandler<UpdateLeaveTimeCommand, LeaveTimeResponse>
{
    private readonly ILeaveTimeRepository _repository = repository;

    public async Task<LeaveTimeResponse> Handle(UpdateLeaveTimeCommand request, CancellationToken cancellationToken)
    {
        var leaveTime = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (leaveTime == null)
            throw new LeaveTimeNotFoundException(request.Id);

        var reason = Enum.Parse<Reason>(request.Item.Reason!);
        var startDate = DateTime.Parse(request.Item.StartDate!);
        var endDate = DateTime.Parse(request.Item.EndDate!);

        leaveTime.Update(request.Item.EmployeeName, reason, startDate, endDate, request.Item.Comment);

        await _repository.UpdateAsync(leaveTime, cancellationToken);

        return leaveTime.MapToDto();
    }
}
