﻿namespace ModernApi.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Model;

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

        if (details == null) return NotFound();

        return Ok(details);
    }
}