using System.ComponentModel.DataAnnotations;

namespace LeaveTimes.Infrastructure;

internal class DatabaseSettings
{
    [Required]
    public string ConnectionString { get; set; } = default!;
}