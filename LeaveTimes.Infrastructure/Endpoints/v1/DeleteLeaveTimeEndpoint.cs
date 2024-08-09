using LeaveTimes.Application.Features.LeaveTimes.Delete;

namespace LeaveTimes.Infrastructure.Endpoints.v1;

public static class DeleteLeaveTimeEndpoint
{
    internal static RouteHandlerBuilder MapLeaveTimeDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (Guid id, ISender mediator) =>
            {
                await mediator.Send(new DeleteLeaveTimeCommand(id));
                return Results.NoContent();
            })
            .WithName(nameof(DeleteLeaveTimeEndpoint))
            .WithSummary("Delete a leave time by its ID.")
            .Produces(204)
            .Produces<ExceptionDetails>(400, "application/json")
            .Produces<ExceptionDetails>(404, "application/json")
            .MapToApiVersion(1);
    }
}
