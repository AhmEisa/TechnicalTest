using System.Collections.Generic;
using System.Threading.Tasks;
using GET.Core.Application.Features.Lookups;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GET.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LookupsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LookupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("services", Name = "GetAllServiceLookups")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ServiceVm>>> GetAllServiceLookups()
        {
            var response = await _mediator.Send(new GetServiceListQuery());
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpGet("users", Name = "GetAllUserLookups")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ServiceVm>>> GetAllUserLookups()
        {
            var response = await _mediator.Send(new GetUserListQuery());
            return StatusCode((int)response.HttpCode, response);
        }
    }
}