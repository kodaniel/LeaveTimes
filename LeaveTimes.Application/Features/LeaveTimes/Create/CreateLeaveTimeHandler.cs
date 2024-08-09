using LeaveTimes.Application.Features.LeaveTimes.Search;

namespace LeaveTimes.Application.Features.LeaveTimes.Create;

public sealed class CreateLeaveTimeHandler(ILeaveTimeRepository repository) : IRequestHandler<CreateLeaveTimeCommand, LeaveTimeResponse>
{
    private readonly ILeaveTimeRepository _repository = repository;

    public async Task<LeaveTimeResponse> Handle(CreateLeaveTimeCommand request, CancellationToken cancellationToken)
    {
        var reason = Enum.Parse<Reason>(request.Reason!, true);
        var leaveTime = LeaveTime.Create(request.EmployeeName!, reason, request.StartDate!.Value, request.EndDate!.Value, request.Comment);

        await _repository.AddAsync(leaveTime, cancellationToken);

        return leaveTime.Adapt<LeaveTimeResponse>();
    }
}
