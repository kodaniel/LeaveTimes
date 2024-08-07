using LeaveTimes.Domain.Entities;
using LeaveTimes.Infrastructure.Context;

namespace LeaveTimes.Infrastructure.Repositories;

public static class SeedData
{
    public static void Initialize(AppDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        if (dbContext.LeaveTimes.Any())
        {
            return;
        }

        dbContext.LeaveTimes.AddRange([
            CreateModel("John", new DateTime(2024, 8, 7), new DateTime(2024, 8, 8), Reason.PaidLeave, "Random text comes here."),
            CreateModel("Jane", new DateTime(2024, 8, 5), new DateTime(2024, 8, 7), Reason.HomeOffice),
            CreateModel("Kutya", new DateTime(2024, 7, 10), new DateTime(2024, 7, 18), Reason.Holiday),
        ]);

        dbContext.SaveChanges();
    }

    private static LeaveTime CreateModel(string employeeName, DateTime startDate, DateTime endDate, Reason reason, string? comment = null)
    {
        var model = LeaveTime.Create(employeeName);
        model.UpdateReason(reason);
        model.UpdateTimes(startDate, endDate);
        model.UpdateComment(comment);

        return model;
    }
}
