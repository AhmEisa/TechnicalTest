using GET.Core.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GET.Core.Application.Features.Users.Authentication
{
    public class RegisterUserCommand : IRequest<ReturnResult<bool>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid ServiceId { get; set; }
    }
}
