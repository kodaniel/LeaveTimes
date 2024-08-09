using LeaveTimes.Application.Features.LeaveTimes.Search;

namespace LeaveTimes.Application.Mappers;

[Mapper]
public static partial class LeaveTimeMapper
{
    public static partial LeaveTimeResponse MapToDto(this LeaveTime leaveTime);

    public static IEnumerable<LeaveTimeResponse> MapToDto(this IEnumerable<LeaveTime> source) => source.Select(MapToDto);
}
