using AutoMapper;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Application.Contracts;
using GET.Core.Application.Models;
using GET.Core.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class UpdateServiceRequestCommandHandler : IRequestHandler<UpdateServiceRequestCommand, ReturnResult<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Service> _serviceRepository;
        private readonly ILogger<UpdateServiceRequestCommandHandler> _logger;
        private readonly IAsyncRepository<User> _userRepository;
        private readonly ILoggedInUserService _loggedInUser;
        private readonly IAsyncRepository<ServiceRequest> _requestRepository;

        public UpdateServiceRequestCommandHandler(IMapper mapper, IAsyncRepository<ServiceRequest> requestRepository,
                                                   IAsyncRepository<Service> serviceRepository,ILogger<UpdateServiceRequestCommandHandler> logger, 
                                                   IAsyncRepository<User> userRepository, ILoggedInUserService loggedInUser)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository;
            _logger = logger;
            _userRepository = userRepository;
            _loggedInUser = loggedInUser;
            _requestRepository = requestRepository;
        }


        public async Task<ReturnResult<bool>> Handle(UpdateServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<bool>();
            try
            {
                var validator = new UpdateServiceRequestCommandValidator(_userRepository, _serviceRepository, _requestRepository);
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult.Errors.Count > 0)
                {
                    response.BadRequest(validationResult.Errors.Select(r => r.ErrorMessage).ToList());
                    return response;
                }

                var serviceRequest = await _requestRepository.GetByIdAsync(request.Id);
                serviceRequest = _mapper.Map(request, serviceRequest);
                serviceRequest.UpdatedBy = _loggedInUser.UserId;

                await _requestRepository.UpdateAsync(serviceRequest);

                response.Success(true);
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
