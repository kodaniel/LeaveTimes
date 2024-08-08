namespace LeaveTimes.Application.Dtos;

public record LeaveTimeDto
{
    /// <summary>
    /// Guid id of the leave time
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Name of the employee who requested the leave
    /// </summary>
    public string EmployeeName { get; set; } = default!;
    
    /// <summary>
    /// Start date of the leave
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// End date of the leave
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Reason
    /// </summary>
    public Reason Reason { get; set; }

    /// <summary>
    /// Additional text
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Leave time is approved or not
    /// </summary>
    public bool IsApproved { get; set; }
}
