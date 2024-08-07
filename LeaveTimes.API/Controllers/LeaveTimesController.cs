using LeaveTimes.Application.Commands;
using LeaveTimes.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTimes.API.Controllers;

//[ApiVersion("1")]
[Route("leave-times")]
public class LeaveTimesController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllLeaveTimes(GetAllLeaveTimesQuery.Request request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLeaveTimeCommand.Request request)
    {
        var response = await Mediator.Send(request);
        return CreatedAtAction(nameof(GetAllLeaveTimes), new { response });
    }
}
