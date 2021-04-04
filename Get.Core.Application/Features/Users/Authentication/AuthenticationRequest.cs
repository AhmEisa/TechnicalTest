using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GET.Core.Application.Models.Authentication
{
    public class AuthenticationRequest : IRequest<ReturnResult<AuthenticationResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
