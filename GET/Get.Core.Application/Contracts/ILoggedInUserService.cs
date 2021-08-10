using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace GET.Core.Application.Contracts
{
    public interface ILoggedInUserService
    {
        string UserId { get; }
        IEnumerable<Claim> UserRoles { get; }
    }
}
