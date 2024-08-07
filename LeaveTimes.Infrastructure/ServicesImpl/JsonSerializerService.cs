using LeaveTimes.Application.Services.Serializer;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LeaveTimes.Infrastructure.ServicesImpl;

internal class JsonSerializerService : ISerializerService
{
    private JsonSerializerOptions _options;

    public JsonSerializerService()
    {
        _options = new JsonSerializerOptions();
        _options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        _options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        _options.WriteIndented = true;
        _options.Converters.Add(new JsonStringEnumConverter());
    }

    public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, _options);

    public string Serialize<T>(T obj, Type type) => JsonSerializer.Serialize(obj, type, _options);

    public T? Deserialize<T>(string text) => JsonSerializer.Deserialize<T>(text, _options);
}
