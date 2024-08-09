using LeaveTimes.Application.Features.LeaveTimes.Search;
using LeaveTimes.Application.Features.LeaveTimes.Update;

namespace LeaveTimes.Infrastructure.Endpoints.v1;

public static class UpdateLeaveTimeEndpoint
{
    internal static RouteHandlerBuilder MapLeaveTimeUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut("/{id:guid}", async (Guid id, UpdateLeaveTimeCommand.UpdateLeaveTimeCommandBody request, ISender mediator) =>
            {
                var response = await mediator.Send(new UpdateLeaveTimeCommand(id, request));
                return Results.Ok(response);
            })
            .WithName(nameof(UpdateLeaveTimeEndpoint))
            .WithSummary("Edit an existing leave time.")
            .Produces<LeaveTimeResponse>(200, "application/json")
            .Produces<ExceptionDetails>(400, "application/json")
            .Produces<ExceptionDetails>(404, "application/json")
            .MapToApiVersion(1);
    }
}
