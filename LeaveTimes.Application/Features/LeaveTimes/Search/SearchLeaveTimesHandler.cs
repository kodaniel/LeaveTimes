namespace LeaveTimes.Application.Features.LeaveTimes.Search;

public sealed class SearchLeaveTimesHandler(ILeaveTimeRepository repository) : IRequestHandler<SearchLeaveTimesCommand, ListResponse<LeaveTimeResponse>>
{
    private readonly ILeaveTimeRepository _repository = repository;

    public async Task<ListResponse<LeaveTimeResponse>> Handle(SearchLeaveTimesCommand request, CancellationToken cancellationToken)
    {
        int year = request.Year ?? DateTime.Now.Year;
        int month = request.Month ?? DateTime.Now.Month;
        Reason? reason = null;
        if (Enum.TryParse(request.Reason, ignoreCase: true, out Reason r))
            reason = r;

        var leaveTimes = await _repository.FilteredListAsync(year, month, request.EmployeeName, reason, cancellationToken);

        return new ListResponse<LeaveTimeResponse>(leaveTimes.MapToDto());
    }
}
