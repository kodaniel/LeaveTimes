using LeaveTimes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaveTimes.Infrastructure.Context;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    #region DbSets

    public DbSet<LeaveTime> LeaveTimes => Set<LeaveTime>();

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Register every classes which implements IEntityTypeConfiguration<> in this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
