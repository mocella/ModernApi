namespace ModernApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MediatR;
    using Model;

    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMediator                   _mediator;

        public MessagesController(
            ILogger<MessagesController> logger, 
            IMediator mediator)
        {
            _logger   = logger;
            _mediator = mediator;
        }

        [HttpGet(Name = "GetMessageDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(Guid messageGuid)
        {
            var details = await _mediator.Send(new GetMessageDetails(messageGuid));

            if (details == null)
            {
                return NotFound();
            }

            return Ok(details);
        }
    }
}
