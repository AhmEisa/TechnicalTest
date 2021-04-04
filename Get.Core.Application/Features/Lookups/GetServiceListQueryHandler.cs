using AutoMapper;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Application.Contracts;
using GET.Core.Application.Models;
using GET.Core.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.Lookups
{
    public class GetServiceListQueryHandler : IRequestHandler<GetServiceListQuery, ReturnResult<List<ServiceVm>>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Service> _serviceRepository;
        private readonly ILoggedInUserService _loggedInUser;
        private readonly ILogger<GetServiceListQueryHandler> _logger;

        public GetServiceListQueryHandler(IMapper mapper, IAsyncRepository<Service> serviceRepository, ILoggedInUserService loggedInUser, ILogger<GetServiceListQueryHandler> logger)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository;
            _loggedInUser = loggedInUser;
            _logger = logger;
        }
        public async Task<ReturnResult<List<ServiceVm>>> Handle(GetServiceListQuery request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<List<ServiceVm>>();
            try
            {
                var allServices = await _serviceRepository.ListAllAsync();

                if (allServices == null || allServices.Count == 0)
                {
                    response.DefaultNotFound();
                    return response;
                }

                response.Success(_mapper.Map<List<ServiceVm>>(allServices));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                response.DefaultServerError();
            }
            return response;
        }
    }
}
