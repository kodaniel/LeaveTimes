using LeaveTimes.Application.Features.LeaveTimes.Create;
using LeaveTimes.Application.Features.LeaveTimes.Search;

namespace LeaveTimes.Infrastructure.Endpoints.v1;

public static class CreateLeaveTimeEndpoint
{
    internal static RouteHandlerBuilder MapLeaveTimeCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (CreateLeaveTimeCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.CreatedAtRoute(nameof(SearchLeaveTimesEndpoint), null, response);
            })
            .WithName(nameof(CreateLeaveTimeEndpoint))
            .WithSummary("Add a new leave time.")
            .Produces<LeaveTimeResponse>(200, "application/json")
            .Produces<ExceptionDetails>(400, "application/json")
            .MapToApiVersion(1);
    }
}
