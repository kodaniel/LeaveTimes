namespace LeaveTimes.Domain.Repositories;

public interface ILeaveTimeRepository : IRepository<LeaveTime>
{
    Task<List<LeaveTime>> ListOrderedAsync(CancellationToken cancellationToken = default);
}
