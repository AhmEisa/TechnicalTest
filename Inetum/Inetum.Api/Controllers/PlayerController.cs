using Inetum.Application.Features.Players.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inetum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddPlayer")]
        public async Task<ActionResult> Create([FromBody] AddPlayerCommand addPlayerCommand)
        {
            var response = await _mediator.Send(addPlayerCommand);
            return Ok(response);
        }

        [HttpPut(Name = "UpdatePlayer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdatePlayerCommand updatePlayerCommand)
        {
            await _mediator.Send(updatePlayerCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeletePlayer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeletePlayerCommand() { PlayerId = id };
            await _mediator.Send(deleteEventCommand);
            return NoContent();
        }
    }
}
