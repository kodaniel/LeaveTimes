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

    public static LeaveTime Create(string name, Reason reason, DateTime startDate, DateTime endDate, string? comment = null)
    {
        var leaveTime = new LeaveTime();
        leaveTime.Update(name, reason, startDate, endDate, comment);
        leaveTime.IsApproved = false;

        return leaveTime;
    }

    public void Update(string? name, Reason? reason, DateTime? startDate, DateTime? endDate, string? comment)
    {
        if (name is not null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(name));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 100);
            EmployeeName = name;
        }

        if (reason is not null)
        {
            Reason = reason.Value;
        }

        if (startDate is not null || endDate is not null)
        {
            if (startDate is not null)
                StartDate = startDate.Value;

            if (endDate is not null)
                EndDate = endDate.Value;

            ArgumentOutOfRangeException.ThrowIfGreaterThan(StartDate, EndDate, nameof(startDate));
        }

        if (comment is not null)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(comment.Length, 500);
            Comment = comment;
        }
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
