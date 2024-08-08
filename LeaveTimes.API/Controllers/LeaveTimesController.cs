using Asp.Versioning;
using LeaveTimes.Application.Commands;
using LeaveTimes.Application.Dtos;
using LeaveTimes.Application.Queries;
using LeaveTimes.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTimes.API.Controllers;

[ApiVersion("1.0")]
[Route("leave-times")]
public class LeaveTimesController : ApiControllerBase
{
    /// <summary>
    /// Gets all the leaves in the given month, or the current month if nothing is provided.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ListResponse<LeaveTimeDto>), 200)]
    [ProducesResponseType(typeof(ExceptionDetails), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> GetAllLeaveTimes(GetAllLeaveTimesQuery.Request request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Add a new leave time.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(LeaveTimeDto), 201)]
    [ProducesResponseType(typeof(ExceptionDetails), 400)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateLeaveTimeCommand.Request request)
    {
        var response = await Mediator.Send(request);
        return CreatedAtAction(nameof(GetAllLeaveTimes), new { response });
    }

    /// <summary>
    /// Edit an existing leave time.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(LeaveTimeDto), 200)]
    [ProducesResponseType(typeof(ExceptionDetails), 400)]
    [ProducesResponseType(typeof(ExceptionDetails), 404)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, UpdateLeaveTimeDto command)
    {
        var response = await Mediator.Send(new UpdateLeaveTimeCommand.Request(id, command));
        return Ok(response);
    }

    /// <summary>
    /// Delete a leave time by its ID.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(202)]
    [ProducesResponseType(typeof(ExceptionDetails), 400)]
    [ProducesResponseType(typeof(ExceptionDetails), 404)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteLeaveTime.Command(id));
        return NoContent();
    }
}
