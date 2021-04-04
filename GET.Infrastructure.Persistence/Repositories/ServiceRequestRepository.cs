using Get.Core.Application.Contracts.Persistence;
using GET.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GET.Infrastructure.Persistence.Repositories
{
    public class ServiceRequestRepository : BaseRepository<ServiceRequest>, IServiceRequestRepository
    {
        public ServiceRequestRepository(GETDbContext dbContext) : base(dbContext) { }

        public async Task<List<ServiceRequest>> GetAllUserRequestsAsync()
        {
            return await _dbContext.ServiceRequest.Include(c => c.User)
                                                  .Include(c => c.Service)
                                                  .Include(c => c.ServiceStatus)
                                                  .ToListAsync();
        }
        public async Task<List<ServiceRequest>> GetByUserRequestsAsync(Guid userId)
        {
            return await _dbContext.ServiceRequest.Include(c => c.Service)
                                                  .Include(c => c.ServiceStatus)
                                                  .Where(sr => sr.UserId == userId).ToListAsync();
        }
    }
}
