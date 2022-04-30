namespace ModernApi.Controllers;

using Api.MessageDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetMessageDetails")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(Guid messageGuid)
    {
        var details = await _mediator.Send(new GetMessageDetails(messageGuid));

        return details == null ? NotFound() : Ok(details);
    }
}