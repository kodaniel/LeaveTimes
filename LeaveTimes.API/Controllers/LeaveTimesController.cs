using LeaveTimes.Application.Commands;
using LeaveTimes.Application.Dtos;
using LeaveTimes.Application.Queries;
using LeaveTimes.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTimes.API.Controllers;

//[ApiVersion("1")]
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
    /// Creates a new leave time.
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
}
