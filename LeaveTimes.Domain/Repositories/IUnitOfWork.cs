namespace LeaveTimes.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    ILeaveTimeRepository LeaveTimeRepository { get; }

    bool HasActiveTransaction { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
