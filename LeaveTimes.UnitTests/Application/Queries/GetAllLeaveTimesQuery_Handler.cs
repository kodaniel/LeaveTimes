using LeaveTimes.Application.Queries;
using LeaveTimes.Domain.Entities;
using LeaveTimes.Domain.Repositories;
using Moq;

namespace LeaveTimes.UnitTests.Application.Queries;

[TestFixture]
public class GetAllLeaveTimesQuery_Handler
{
    private GetAllLeaveTimesQuery.Handler handler;
    private Mock<ILeaveTimeRepository> repositoryMock;

    [SetUp]
    public void SetUp()
    {
        // Or can be made a LeaveTimeRepositoryMock class which implements ILeaveTimeRepository...
        repositoryMock = new Mock<ILeaveTimeRepository>();
        repositoryMock.Setup(x => x.FilteredListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<Reason?>(), It.IsAny<CancellationToken>()))
            .Returns<int, int, string?, Reason?, CancellationToken>((y, m, n, r, _) => Task.FromResult(GetList(y, m, n, r)));

        handler = new GetAllLeaveTimesQuery.Handler(repositoryMock.Object);
    }

    [Test]
    public async Task With_Current_Year_And_Month()
    {
        var requestModel = new GetAllLeaveTimesQuery.Request();
        var result = await handler.Handle(requestModel, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(3));
    }

    [Test]
    public async Task With_Specific_Reason()
    {
        var requestModel = new GetAllLeaveTimesQuery.Request { Reason = "Holiday" };
        var result = await handler.Handle(requestModel, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    private List<LeaveTime> GetList(int year, int month, string? employeeName, Reason? reason)
    {
        var list = new List<LeaveTime>
        {
            CreateModel("aaa", Reason.Holiday, new DateTime(2024, 8, 10), new DateTime(2024, 8, 10)),
            CreateModel("bbb", Reason.HomeOffice, new DateTime(2024, 8, 29), new DateTime(2024, 9, 5)),
            CreateModel("ccc", Reason.Holiday, new DateTime(2024, 7, 1), new DateTime(2024, 9, 30)),
        };

        if (!string.IsNullOrEmpty(employeeName))
        {
            list = list.Where(item => item.EmployeeName.Contains(employeeName, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        if (reason is not null)
        {
            list = list.Where(item => item.Reason == reason).ToList();
        }

        return list;
    }

    private LeaveTime CreateModel(string name, Reason reason, DateTime startDate, DateTime endDate)
    {
        var model = LeaveTime.Create(name);
        model.UpdateReason(reason);
        model.UpdateTimes(startDate, endDate);

        return model;
    }
}
