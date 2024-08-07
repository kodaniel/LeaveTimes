namespace LeaveTimes.Domain.Repositories;

#region Read Repository

public interface IReadRepository<TEntity> : IReadRepository<TEntity, Guid>
    where TEntity : class, IAggregateRoot
{
}

public interface IReadRepository<TEntity, in TKey>
    where TEntity : class, IAggregateRoot
    where TKey : IEquatable<TKey>
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default);
    Task<long> CountAsync(CancellationToken cancellationToken = default);
}

#endregion

#region Write Repository

public interface IWriteRepository<TEntity> : IWriteRepository<TEntity, Guid>
    where TEntity : class, IAggregateRoot
{
}

public interface IWriteRepository<TEntity, in TKey>
    where TEntity : class, IAggregateRoot
    where TKey : IEquatable<TKey>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}

#endregion

#region Read-Write Repository

public interface IRepository<TEntity> : IRepository<TEntity, Guid>
    where TEntity : class, IAggregateRoot
{
}

public interface IRepository<TEntity, in TKey> : IWriteRepository<TEntity, TKey>, IReadRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot
    where TKey : IEquatable<TKey>
{
}

#endregion
