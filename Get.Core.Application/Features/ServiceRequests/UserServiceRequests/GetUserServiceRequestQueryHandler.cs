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
    public class GetUserServiceRequestQueryHandler : IRequestHandler<GetUserServiceRequestQuery, ReturnResult<List<SeriveRequestVm>>>
    {
        private readonly IMapper _mapper;
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly ILogger<GetUserServiceRequestQueryHandler> _logger;

        public GetUserServiceRequestQueryHandler(IMapper mapper, IServiceRequestRepository serviceRequestRepository,
                                                 ILoggedInUserService loggedInUserService, ILogger<GetUserServiceRequestQueryHandler> logger)
        {
            _mapper = mapper;
            _serviceRequestRepository = serviceRequestRepository;
            _loggedInUserService = loggedInUserService;
            _logger = logger;
        }
        public async Task<ReturnResult<List<SeriveRequestVm>>> Handle(GetUserServiceRequestQuery request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<List<SeriveRequestVm>>();
            try
            {
                request.UserId = new Guid(_loggedInUserService.UserId);
                var userRequests = await _serviceRequestRepository.GetByUserRequestsAsync(request.UserId);

                if (userRequests != null && userRequests.Count > 0)
                    response.Success(_mapper.Map<List<SeriveRequestVm>>(userRequests));
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
