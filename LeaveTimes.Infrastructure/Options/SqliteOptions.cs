using System.ComponentModel.DataAnnotations;

namespace LeaveTimes.Infrastructure.Options;

public class SqliteOptions
{
    [Required]
    public string ConnectionString { get; set; } = default!;
}