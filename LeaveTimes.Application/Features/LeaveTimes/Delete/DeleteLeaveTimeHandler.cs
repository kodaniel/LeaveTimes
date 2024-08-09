namespace LeaveTimes.Application.Features.LeaveTimes.Delete;

public sealed class DeleteLeaveTimeHandler(ILeaveTimeRepository repository) : IRequestHandler<DeleteLeaveTimeCommand>
{
    private readonly ILeaveTimeRepository _repository = repository;

    public async Task Handle(DeleteLeaveTimeCommand request, CancellationToken cancellationToken)
    {
        var leaveTime = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (leaveTime == null)
            throw new LeaveTimeNotFoundException(request.Id);

        if (leaveTime.IsApproved)
            throw new LeaveTimeAlreadyApprovedException();

        await _repository.DeleteAsync(leaveTime, cancellationToken);
    }
}
