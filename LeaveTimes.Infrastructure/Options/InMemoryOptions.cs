using System.ComponentModel.DataAnnotations;

namespace LeaveTimes.Infrastructure.Options;

public class InMemoryOptions
{
    [Required]
    public string Name { get; set; } = default!;
}
