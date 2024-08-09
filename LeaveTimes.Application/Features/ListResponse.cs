using System.Text.Json.Serialization;

namespace LeaveTimes.Application.Features;

public class ListResponse<T>
{
    [JsonPropertyName("items")]
    public IEnumerable<T> Items { get; set; }

    [JsonPropertyName("count")]
    public long Count => Items.LongCount();

    public ListResponse() : this([])
    {
    }

    public ListResponse(IEnumerable<T> data)
    {
        Items = data;
    }
}
