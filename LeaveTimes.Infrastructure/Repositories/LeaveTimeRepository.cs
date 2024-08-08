using LeaveTimes.Domain.Entities;
using LeaveTimes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LeaveTimes.Infrastructure.Repositories;

internal class LeaveTimeRepository(AppDbContext dbContext) : EFRepositoryBase<LeaveTime>(dbContext), ILeaveTimeRepository
{
    public async Task<List<LeaveTime>> FilteredListAsync(int year, int month, string? employeeName = default, Reason? reason = default,
        CancellationToken cancellationToken = default)
    {
        var startOfMonth = new DateTime(year, month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1);

        // Build the query with filters

        var query = DbSet.AsNoTracking()
            // Filter records where the leave time is colliding with the month.
            // It's required for leave times which lasting several months, e.g. end of the month or very long leaves.
            .Where(x => x.StartDate <= endOfMonth && x.EndDate >= startOfMonth)
            .WhereIf(!string.IsNullOrWhiteSpace(employeeName), x => x.EmployeeName.Contains(employeeName!, StringComparison.InvariantCultureIgnoreCase))
            .WhereIf(reason is not null, x => x.Reason == reason)
            .OrderBy(x => x.StartDate);

#if DEBUG // Just to see what EF Core has generated
        var sql = query.ToQueryString();
#endif

        return await query.ToListAsync(cancellationToken);
    }
}
