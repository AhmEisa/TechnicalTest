using AutoMapper;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Application.Contracts;
using GET.Core.Application.Models;
using GET.Core.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class CreateServiceRequestCommandHandler : IRequestHandler<CreateServiceRequestCommand, ReturnResult<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Service> _serviceRepository;
        private readonly ILogger<CreateServiceRequestCommandHandler> _logger;
        private readonly IAsyncRepository<User> _userRepository;
        private readonly ILoggedInUserService _loggedInUser;
        private readonly IAsyncRepository<ServiceRequest> _requestRepository;

        public CreateServiceRequestCommandHandler(IMapper mapper, IAsyncRepository<ServiceRequest> requestRepository,
                                                   IAsyncRepository<Service> serviceRepository, ILogger<CreateServiceRequestCommandHandler> logger,
                                                   IAsyncRepository<User> userRepository, ILoggedInUserService loggedInUser)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository;
            _logger = logger;
            _userRepository = userRepository;
            _loggedInUser = loggedInUser;
            _requestRepository = requestRepository;
        }


        public async Task<ReturnResult<Guid>> Handle(CreateServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<Guid>();
            try
            {
                var validator = new CreateServiceRequestCommandValidator(_userRepository, _serviceRepository);
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult.Errors.Count > 0)
                {
                    response.BadRequest(validationResult.Errors.Select(r => r.ErrorMessage).ToList());
                    return response;
                }

                var serviceRequest = _mapper.Map<ServiceRequest>(request);
                serviceRequest.CreatedBy = _loggedInUser.UserId;

                serviceRequest = await _requestRepository.AddAsync(@serviceRequest);

                response.Success(serviceRequest.Id);
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
