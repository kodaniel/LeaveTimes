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

    public async Task<List<LeaveTime>> FilteredListAsync(int year, int month, string? employeeName = default, Reason? reason = default,
        CancellationToken cancellationToken = default)
    {
        var firstDayOfMonth = new DateTime(year, month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddTicks(-1);

        // Build the query

        var query = DbSet.AsNoTracking();

        query = query.Where(x => x.StartDate <= lastDayOfMonth && x.EndDate >= firstDayOfMonth);

        if (!string.IsNullOrWhiteSpace(employeeName))
        {
            query = query.Where(x => x.EmployeeName.Contains(employeeName, StringComparison.InvariantCultureIgnoreCase));
        }

        if (reason != null)
        {
            query = query.Where(x => x.Reason == reason);
        }

        query = query.OrderBy(x => x.StartDate);

        return await query.ToListAsync(cancellationToken);
    }
}
