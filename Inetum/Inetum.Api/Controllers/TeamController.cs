using Inetum.Application.Features.Teams.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inetum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize]
        [HttpGet("all", Name = "GetAllTeams")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TeamListVm>>> GetAllTeams()
        {
            var dtos = await _mediator.Send(new GetTeamListQuery());
            return Ok(dtos);
        }

        // [Authorize]
        [HttpGet("allwithplayers", Name = "GetTeamsWithPlayers")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TeamListWithPlayersVm>>> GetTeamsWithPlayers(bool includePlayers)
        {
            var dtos = await _mediator.Send(new GetTeamListWithPlayersQuery() { IncludePlayers = includePlayers });
            return Ok(dtos);
        }

        [HttpGet("teamwithplayers", Name = "GetTeamWithPlayers")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TeamListWithPlayersVm>> GetTeamWithPlayers(Guid id)
        {
            var dtos = await _mediator.Send(new GetTeamWithPlayersQuery() { TeamId = id });
            return Ok(dtos);
        }

        [HttpPost(Name = "AddTeam")]
        public async Task<ActionResult<CreateTeamCommandResponse>> Create([FromBody] CreateTeamCommand createCategoryCommand)
        {
            var response = await _mediator.Send(createCategoryCommand);
            return Ok(response);
        }

        [HttpPut(Name = "UpdateTeam")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateTeamCommand updateEventCommand)
        {
            await _mediator.Send(updateEventCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTeam")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteTeamCommand() { TeamId = id };
            await _mediator.Send(deleteEventCommand);
            return NoContent();
        }
    }
}
