using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace LeaveTimes.Application.Features.LeaveTimes.Search;

public sealed record SearchLeaveTimesCommand(
    int? Year,
    int? Month,
    string? EmployeeName,
    string? Reason) : IRequest<ListResponse<LeaveTimeResponse>>
{
    // This method is required only for Minimal API to extract the query string
    public static ValueTask<SearchLeaveTimesCommand?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        int? year = int.TryParse(context.Request.Query["year"], out var year_) ? year_ : null;
        int? month = int.TryParse(context.Request.Query["month"], out var month_) ? month_ : null;
        string? employeeName = context.Request.Query["employeeName"];
        string? reason = context.Request.Query["reason"];

        var result = new SearchLeaveTimesCommand(year, month, employeeName, reason);
        return ValueTask.FromResult<SearchLeaveTimesCommand?>(result);
    }
}
