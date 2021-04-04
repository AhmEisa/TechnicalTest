using GET.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Get.Core.Application.Contracts.Persistence
{
    public interface IServiceRequestRepository : IAsyncRepository<ServiceRequest>
    {
        Task<List<ServiceRequest>> GetByUserRequestsAsync(Guid userId);
        Task<List<ServiceRequest>> GetAllUserRequestsAsync();
    }
}
