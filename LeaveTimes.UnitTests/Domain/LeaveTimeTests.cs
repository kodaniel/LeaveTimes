using LeaveTimes.Domain.Entities;

namespace LeaveTimes.UnitTests.Domain;

[TestFixture]
public class LeaveTimeTests
{
    private string _name = "testname";
    private Reason _reason = Reason.HomeOffice;
    private DateTime _startDate = DateTime.Now;
    private DateTime _endDate = DateTime.Now.AddDays(1);
    private string _comment = "testcomment";

    private LeaveTime CreateDefaultModel() => LeaveTime.Create(_name, _reason, _startDate, _endDate, _comment);

    [Test]
    public void Initializes()
    {
        var entity = CreateDefaultModel();

        Assert.That(entity, Is.Not.Null);
        Assert.That(entity.EmployeeName, Is.EqualTo(_name));
        Assert.That(entity.Reason, Is.EqualTo(_reason));
        Assert.That(entity.StartDate, Is.EqualTo(_startDate));
        Assert.That(entity.EndDate, Is.EqualTo(_endDate));
        Assert.That(entity.Comment, Is.EqualTo(_comment));
    }

    [Test]
    public void Update_Name_is_too_long()
    {
        var entity = CreateDefaultModel();
        var newName = new string('a', 101);

        Assert.Throws<ArgumentOutOfRangeException>(() => entity.Update(newName, null, null, null, null));
    }

    [Test]
    public void Update_EndDate_is_less_than_StartDate()
    {
        var entity = CreateDefaultModel();
        var endDate = _startDate.AddDays(-1);

        Assert.Throws<ArgumentOutOfRangeException>(() => entity.Update(null, null, null, endDate, null));
    }

    [Test]
    public void Update_StartDate_is_later_than_EndDate()
    {
        var entity = CreateDefaultModel();
        var startDate = _endDate.AddDays(1);

        Assert.Throws<ArgumentOutOfRangeException>(() => entity.Update(null, null, startDate, null, null));
    }

    [Test]
    public void Update_Comment_is_too_long()
    {
        var entity = CreateDefaultModel();
        var newComment = new string('a', 501);

        Assert.Throws<ArgumentOutOfRangeException>(() => entity.Update(null, null, null, null, newComment));
    }
}
