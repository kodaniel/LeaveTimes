using LeaveTimes.Domain.Entities;
using LeaveTimes.Domain.Repositories;
using LeaveTimes.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LeaveTimes.Infrastructure.Repositories;

internal class LeaveTimeRepository : EFRepositoryBase<LeaveTime>, ILeaveTimeRepository
{
    public LeaveTimeRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<LeaveTime>> ListOrderedAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .OrderByDescending(x => x.StartDate)
            .ToListAsync(cancellationToken);
    }
}
