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
    public class UpdateServiceRequestStatusCommandHandler : IRequestHandler<UpdateServiceRequestStatusCommand, ReturnResult<bool>>
    {
        private readonly IAsyncRepository<ServiceRequest> _requestRepository;
        private readonly ILoggedInUserService _loggedInUser;
        private readonly ILogger<UpdateServiceRequestStatusCommandHandler> _logger;

        public UpdateServiceRequestStatusCommandHandler(IAsyncRepository<ServiceRequest> requestRepository, ILoggedInUserService loggedInUser,
                                                        ILogger<UpdateServiceRequestStatusCommandHandler> logger)
        {
            _requestRepository = requestRepository;
            _loggedInUser = loggedInUser;
            _logger = logger;
        }

        public async Task<ReturnResult<bool>> Handle(UpdateServiceRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<bool>();
            try
            {
                var validator = new UpdateServiceRequestStatusCommandValidator(_requestRepository);

                var validationResult = validator.Validate(request);
                if (validationResult.Errors.Count > 0)
                {
                    response.BadRequest(validationResult.Errors.Select(r => r.ErrorMessage).ToList());
                    return response;
                }

                var serviceRequest = await _requestRepository.GetByIdAsync(request.RequestId);

                serviceRequest.StatusId = request.StatusId;
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
