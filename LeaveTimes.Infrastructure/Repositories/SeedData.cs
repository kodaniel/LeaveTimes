using LeaveTimes.Domain.Entities;

namespace LeaveTimes.Infrastructure.Repositories;

public static class SeedData
{
    public static void PopulateData(AppDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        if (dbContext.LeaveTimes.Any())
        {
            return;
        }

        dbContext.LeaveTimes.AddRange([
            LeaveTime.Create("Eddard \"Ned\" Stark", Reason.PaidLeave,      new DateTime(2024, 8, 7),  new DateTime(2024, 8, 8), "Random text comes here."),
            LeaveTime.Create("Robert Baratheon",     Reason.HomeOffice,     new DateTime(2024, 8, 5),  new DateTime(2024, 8, 7)),
            LeaveTime.Create("Jaime Lannister",      Reason.BusinessTravel, new DateTime(2024, 7, 10), new DateTime(2024, 7, 18)),
            LeaveTime.Create("Daenerys Targaryen",   Reason.Holiday,        new DateTime(2024, 8, 10), new DateTime(2024, 8, 20), "You stand in the presence of Daenerys Stormborn of House Targaryen, rightful heir to the Iron Throne, rightful Queen of the Andals, and the First Men, Protector of the Seven Kingdoms, the Mother of Dragons, the Khaleesi of the Great Grass Sea, the Unburnt, the Breaker of Chains."),
            LeaveTime.Create("Jon Snow",             Reason.Holiday,        new DateTime(2024, 8, 15), new DateTime(2024, 9, 20), "This is Jon Snow, king of the north."),
            LeaveTime.Create("Robb Stark",           Reason.NonPaidLeave,   new DateTime(2024, 7, 10), new DateTime(2024, 9, 20)),
            LeaveTime.Create("Theon Greyjoy",        Reason.BusinessTravel, new DateTime(2023, 6, 10), new DateTime(2024, 8, 30)),
            LeaveTime.Create("Joffrey Baratheon",    Reason.NonPaidLeave,   new DateTime(2024, 8, 1),  new DateTime(2024, 8, 1)),
        ]);

        dbContext.SaveChanges();
    }
}
