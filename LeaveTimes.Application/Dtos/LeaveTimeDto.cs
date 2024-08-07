using System.Text.Json.Serialization;

namespace LeaveTimes.Application.Dtos;

public record LeaveTimeDto
{
    public Guid Id { get; set; }
    
    public string EmployeeName { get; set; } = default!;
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public Reason Reason { get; set; }

    public string? Comments { get; set; }
}
