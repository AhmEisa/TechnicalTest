using Inetum.Domain.Common;
using Inetum.Domain.Entities;
using Inetum.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inetum.Identity
{
    public class InetumIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public InetumIdentityDbContext(DbContextOptions<InetumIdentityDbContext> options) : base(options)
        {
        }

        public DbSet<Team> Team { get; set; }
        public DbSet<Player> Player { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Player>().HasOne(e => e.Team).WithMany(e => e.Players).OnDelete(DeleteBehavior.Cascade);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "Admin";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "Admin";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
