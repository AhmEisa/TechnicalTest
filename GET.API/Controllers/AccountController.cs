using System.Threading.Tasks;
using GET.Core.Application.Features.Users.Authentication;
using GET.Core.Application.Models;
using GET.Core.Application.Models.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GET.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegisterUserCommand request)
        {
            var response = await _mediator.Send(request);
            return StatusCode((int)response.HttpCode, response);
        }
    }
}