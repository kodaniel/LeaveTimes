using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTimes.API.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
