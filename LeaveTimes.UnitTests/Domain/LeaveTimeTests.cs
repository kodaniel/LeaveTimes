using LeaveTimes.Domain.Entities;

namespace LeaveTimes.UnitTests.Domain;

[TestFixture]
public class LeaveTimeTests
{
    private readonly string name = "testname";

    private LeaveTime CreateLeaveTime() => LeaveTime.Create(name);

    [Test]
    public void InitializesWithName()
    {
        var entity = CreateLeaveTime();

        Assert.That(entity, Is.Not.Null);
        Assert.That(entity.EmployeeName, Is.EqualTo(name));
    }

    [Test]
    public void UpdateName_Ok()
    {
        var entity = CreateLeaveTime();
        var newName = "New name";

        entity.UpdateName(newName);

        Assert.That(entity.EmployeeName, Is.EqualTo(newName));
    }

    [Test]
    public void UpdateName_TooLong()
    {
        var entity = CreateLeaveTime();
        var newName = new string('a', 101);

        Assert.Throws<ArgumentOutOfRangeException>(() => entity.UpdateName(newName));
    }

    [Test]
    public void UpdateTimes_Ok()
    {
        var entity = CreateLeaveTime();
        var startDate = new DateTime(2024, 8, 10);
        var endDate = new DateTime(2024, 8, 10);

        entity.UpdateTimes(startDate, endDate);

        Assert.That(entity.StartDate, Is.EqualTo(startDate));
        Assert.That(entity.EndDate, Is.EqualTo(endDate));
    }

    [Test]
    public void UpdateTimes_EndDateIsLess()
    {
        var entity = CreateLeaveTime();
        var startDate = new DateTime(2024, 8, 10);
        var endDate = new DateTime(2024, 8, 9);

        Assert.Throws<ArgumentOutOfRangeException>(() => entity.UpdateTimes(startDate, endDate));
    }

    [Test]
    public void UpdateReason_Ok()
    {
        var entity = CreateLeaveTime();
        var newReason = Reason.HomeOffice;

        entity.UpdateReason(newReason);

        Assert.That(entity.Reason, Is.EqualTo(newReason));
    }

    [Test]
    public void UpdateComment_Ok()
    {
        var entity = CreateLeaveTime();
        var newComment = "Lorem ipsum...";

        entity.UpdateComment(newComment);

        Assert.That(entity.Comment, Is.EqualTo(newComment));
    }

    [Test]
    public void UpdateComment_TooLong()
    {
        var entity = CreateLeaveTime();
        var newComment = new string('a', 501);

        Assert.Throws<ArgumentOutOfRangeException>(() => entity.UpdateComment(newComment));
    }
}
