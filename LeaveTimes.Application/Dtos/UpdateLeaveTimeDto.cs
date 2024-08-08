namespace LeaveTimes.Application.Dtos;

public record UpdateLeaveTimeDto
{
    public string EmployeeName { get; set; } = default!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Reason { get; set; }

    public string? Comment { get; set; }
}
