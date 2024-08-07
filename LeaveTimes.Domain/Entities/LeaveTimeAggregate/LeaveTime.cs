namespace LeaveTimes.Domain.Entities;

public class LeaveTime : Entity<Guid>, IAggregateRoot
{
    public string EmployeeName { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public Reason Reason { get; private set; } = Reason.Holiday;

    public string? Comment { get; private set; }

    public bool IsApproved { get; private set; }

    public LeaveTime(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(name));
        EmployeeName = name;
    }

    public void UpdateReason(Reason reason) => Reason = reason;

    public void UpdateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(name));
        EmployeeName = name;
    }

    public void UpdateTimes(DateTime startDate,  DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("The start date must be less than or equal to the end date.");

        StartDate = startDate;
        EndDate = endDate;
    }

    public void UpdateComment(string comment)
    {
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
