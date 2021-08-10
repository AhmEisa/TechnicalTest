using GET.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Infrastructure.Persistence
{
    public class GETDbContext : DbContext
    {
        public GETDbContext(DbContextOptions<GETDbContext> options)
           : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceRequest> ServiceRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GETDbContext).Assembly);

            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Service>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<ServiceRequest>().HasQueryFilter(p => !p.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
