using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace LeaveTimes.Infrastructure.Options;

public class DatabaseSettings : IValidatableObject
{
    public const string InMemory = "in-memory";
    public const string Sqlite = "sqlite";

    private static readonly HashSet<string> providerNames = [InMemory, Sqlite];

    [Required]
    [ConfigurationKeyName("provider")]
    public string DbProvider { get; set; } = default!;

    [ConfigurationKeyName("in-memory")]
    public InMemoryOptions? InMemoryOptions { get; set; }

    [ConfigurationKeyName("sqlite")]
    public SqliteOptions? SqliteOptions { get; set; }

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        if (!IsSupported(DbProvider))
        {
            results.Add(new ValidationResult($"Database provider '{DbProvider}' is not supported. Use one of the following: {GetSupportedProviders()}"));
        }
        else
        {
            object? dbOptions = DbProvider switch
            {
                InMemory => InMemoryOptions,
                Sqlite => SqliteOptions,
                _ => throw new InvalidOperationException()
            };

            if (dbOptions is null)
            {
                results.Add(new ValidationResult($"Database settings for '{DbProvider}' are missing."));
            }
            else
            {
                var context = new ValidationContext(dbOptions);
                Validator.TryValidateObject(dbOptions, context, results, true);
            }
        }

        return results;
    }

    private static bool IsSupported(string providerName) =>
        providerNames.Contains(providerName);

    private static string GetSupportedProviders() =>
        string.Join(", ", providerNames);
}
