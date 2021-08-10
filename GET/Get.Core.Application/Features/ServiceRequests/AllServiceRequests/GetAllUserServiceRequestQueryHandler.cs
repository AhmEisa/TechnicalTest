using AutoMapper;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Application.Contracts;
using GET.Core.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class GetAllUserServiceRequestQueryHandler : IRequestHandler<GetAllUserServiceRequestQuery, ReturnResult<List<SeriveRequestVm>>>
    {
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly ILogger<GetAllUserServiceRequestQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetAllUserServiceRequestQueryHandler(IMapper mapper, IServiceRequestRepository serviceRequestRepository,
                                                    ILogger<GetAllUserServiceRequestQueryHandler> logger)
        {
            _mapper = mapper;
            _serviceRequestRepository = serviceRequestRepository;
            _logger = logger;
        }
        public async Task<ReturnResult<List<SeriveRequestVm>>> Handle(GetAllUserServiceRequestQuery request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<List<SeriveRequestVm>>();
            try
            {
                var allRequests = await _serviceRequestRepository.GetAllUserRequestsAsync();

                if (allRequests != null && allRequests.Count > 0)
                    response.Success(_mapper.Map<List<SeriveRequestVm>>(allRequests));
                else
                    response.DefaultNotFound();
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
