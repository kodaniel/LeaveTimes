using Asp.Versioning;
using LeaveTimes.Application.Features;
using LeaveTimes.Application.Features.LeaveTimes.Create;
using LeaveTimes.Application.Features.LeaveTimes.Delete;
using LeaveTimes.Application.Features.LeaveTimes.Search;
using LeaveTimes.Application.Features.LeaveTimes.Update;
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
    [ProducesResponseType(typeof(ListResponse<LeaveTimeResponse>), 200)]
    [ProducesResponseType(typeof(ExceptionDetails), 400)]
    [Produces("application/json")]
    public async Task<IActionResult> SearchLeaveTimes(SearchLeaveTimesCommand request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Add a new leave time.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(LeaveTimeResponse), 201)]
    [ProducesResponseType(typeof(ExceptionDetails), 400)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> Create([FromBody] CreateLeaveTimeCommand request)
    {
        var response = await Mediator.Send(request);
        return CreatedAtAction(nameof(SearchLeaveTimes), response);
    }

    /// <summary>
    /// Edit an existing leave time.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(LeaveTimeResponse), 200)]
    [ProducesResponseType(typeof(ExceptionDetails), 400)]
    [ProducesResponseType(typeof(ExceptionDetails), 404)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] UpdateLeaveTimeCommand.UpdateLeaveTimeCommandBody command)
    {
        var response = await Mediator.Send(new UpdateLeaveTimeCommand(id, command));
        return Ok(response);
    }

    /// <summary>
    /// Delete a leave time by its ID.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ExceptionDetails), 400)]
    [ProducesResponseType(typeof(ExceptionDetails), 404)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteLeaveTimeCommand(id));
        return NoContent();
    }
}
