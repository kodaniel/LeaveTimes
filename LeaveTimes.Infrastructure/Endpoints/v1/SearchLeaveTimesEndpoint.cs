using LeaveTimes.Application.Features;
using LeaveTimes.Application.Features.LeaveTimes.Search;

namespace LeaveTimes.Infrastructure.Endpoints.v1;

public static class SearchLeaveTimesEndpoint
{
    internal static RouteHandlerBuilder MapLeaveTimeSearchEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/", async (SearchLeaveTimesCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(SearchLeaveTimesEndpoint))
            .WithSummary("Search for leave times.")
            .WithDescription("Gets all the leaves in the given month, or the current month if nothing is provided.")
            .Produces<ListResponse<LeaveTimeResponse>>(200, "application/json")
            .Produces<ExceptionDetails>(400, "application/json")
            .MapToApiVersion(1);
    }
}
