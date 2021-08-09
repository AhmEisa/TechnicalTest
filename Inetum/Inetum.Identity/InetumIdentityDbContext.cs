using Inetum.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inetum.Identity
{
    public class InetumIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public InetumIdentityDbContext(DbContextOptions<InetumIdentityDbContext> options) : base(options)
        {
        }
    }
}
