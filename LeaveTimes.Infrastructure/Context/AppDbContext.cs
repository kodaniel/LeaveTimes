﻿using LeaveTimes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaveTimes.Infrastructure.Context;

internal class AppDbContext : CoreDbContext
{
    #region DbSets

    public DbSet<LeaveTime> LeaveTimes => Set<LeaveTime>();

    #endregion

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
