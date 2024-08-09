namespace LeaveTimes.Application.Features;

public class ListResponse<T>
{
    [JsonProperty("items")]
    public IEnumerable<T> Items { get; }

    [JsonProperty("count")]
    public long Count { get; }

    public ListResponse(IEnumerable<T> data)
    {
        Items = data;
        Count = data.LongCount();
    }
}
