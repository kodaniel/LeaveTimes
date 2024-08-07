using LeaveTimes.Domain.Interfaces;
using LeaveTimes.Domain.Repositories;
using LeaveTimes.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LeaveTimes.Infrastructure.Repositories;

/// <summary>
/// Abstract repository base class for Entity Framework Core.
/// </summary>
internal abstract class EFRepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class, IAggregateRoot
{
    protected readonly DbContext dbContext;

    public EFRepositoryBase(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    protected DbSet<TEntity> DbSet => dbContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync([id], cancellationToken);
    }

    public async Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await DbSet.AddRangeAsync(entities, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.CountAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbSet.UpdateRange(entities);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbSet.RemoveRange(entities);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
