namespace LeaveTimes.Domain.Entities;

public class LeaveTime : Entity<Guid>, IAggregateRoot
{
    public string EmployeeName { get; private set; } = default!;

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public Reason Reason { get; private set; } = Reason.Holiday;

    public string? Comment { get; private set; }

    public bool IsApproved { get; private set; }

    private LeaveTime()
    {
    }

    public static LeaveTime Create(string employeeName)
    {
        LeaveTime leaveTime = new();
        leaveTime.UpdateName(employeeName);

        return leaveTime;
    }

    public void UpdateReason(Reason reason) => Reason = reason;

    public void UpdateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(name));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 100);

        EmployeeName = name;
    }

    public void UpdateTimes(DateTime startDate, DateTime endDate)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startDate, endDate, nameof(endDate));

        StartDate = startDate;
        EndDate = endDate;
    }

    public void UpdateComment(string? comment)
    {
        if (comment is not null)
            ArgumentOutOfRangeException.ThrowIfGreaterThan(comment.Length, 500);

        Comment = comment;
    }

    public void Approve()
    {
        IsApproved = true;
    }

    public void Decline()
    {
        IsApproved = false;
    }
}
