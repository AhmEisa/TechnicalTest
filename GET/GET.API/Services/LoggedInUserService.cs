using GET.Core.Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace GET.API.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserRoles = httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role) ?? new List<Claim>();
        }

        public string UserId { get; }
        public IEnumerable<Claim> UserRoles { get; }
    }
}
