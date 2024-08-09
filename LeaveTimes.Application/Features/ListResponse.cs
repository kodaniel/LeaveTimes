using System.Text.Json.Serialization;

namespace LeaveTimes.Application.Features;

public class ListResponse<T>
{
    [JsonPropertyName("items")]
    public IEnumerable<T> Items { get; }

    [JsonPropertyName("count")]
    public long Count { get; }

    public ListResponse(IEnumerable<T> data)
    {
        Items = data;
        Count = data.LongCount();
    }
}
