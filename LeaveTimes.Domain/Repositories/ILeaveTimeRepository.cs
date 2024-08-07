namespace LeaveTimes.Domain.Repositories;

public interface ILeaveTimeRepository : IRepository<LeaveTime>
{
    Task<List<LeaveTime>> FilteredListAsync(int year, int month, string? employeeName = default, Reason? reason = default, 
        CancellationToken cancellationToken = default);
}
