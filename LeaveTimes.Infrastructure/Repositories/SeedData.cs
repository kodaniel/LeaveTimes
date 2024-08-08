using LeaveTimes.Domain.Entities;

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
            CreateModel("Eddard \"Ned\" Stark", new DateTime(2024, 8, 7), new DateTime(2024, 8, 8), Reason.PaidLeave, "Random text comes here."),
            CreateModel("Robert Baratheon", new DateTime(2024, 8, 5), new DateTime(2024, 8, 7), Reason.HomeOffice),
            CreateModel("Jaime Lannister", new DateTime(2024, 7, 10), new DateTime(2024, 7, 18), Reason.BusinessTravel),
            CreateModel("Daenerys Targaryen", new DateTime(2024, 8, 10), new DateTime(2024, 8, 20), Reason.Holiday, "You stand in the presence of Daenerys Stormborn of House Targaryen, rightful heir to the Iron Throne, rightful Queen of the Andals, and the First Men, Protector of the Seven Kingdoms, the Mother of Dragons, the Khaleesi of the Great Grass Sea, the Unburnt, the Breaker of Chains."),
            CreateModel("Jon Snow", new DateTime(2024, 8, 15), new DateTime(2024, 9, 20), Reason.Holiday, "This is Jon Snow, king of the north."),
            CreateModel("Robb Stark", new DateTime(2024, 7, 10), new DateTime(2024, 9, 20), Reason.NonPaidLeave),
            CreateModel("Theon Greyjoy", new DateTime(2023, 6, 10), new DateTime(2024, 8, 30), Reason.BusinessTravel),
            CreateModel("Joffrey Baratheon", new DateTime(2024, 8, 1), new DateTime(2024, 8, 1), Reason.NonPaidLeave),
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
