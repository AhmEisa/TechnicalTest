using Get.Core.Application.Contracts.Persistence;
using GET.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GET.Infrastructure.Persistence.Repositories
{
    public class ServiceLookupRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceLookupRepository(GETDbContext dbContext) : base(dbContext)
        {
        }
    }
}
