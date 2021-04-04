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
    public class DeleteServiceRequestCommandHandler : IRequestHandler<DeleteServiceRequestCommand, ReturnResult<bool>>
    {
        private readonly IAsyncRepository<ServiceRequest> _requestRepository;
        private readonly ILoggedInUserService _loggedInUser;
        private readonly ILogger<DeleteServiceRequestCommandHandler> _logger;

        public DeleteServiceRequestCommandHandler(IAsyncRepository<ServiceRequest> requestRepository, ILoggedInUserService loggedInUser, ILogger<DeleteServiceRequestCommandHandler> logger)
        {
            _requestRepository = requestRepository;
            _loggedInUser = loggedInUser;
            _logger = logger;
        }
        public async Task<ReturnResult<bool>> Handle(DeleteServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<bool>();
            try
            {
                var existedRequest = await _requestRepository.GetByIdAsync(request.RequestId);
                if (existedRequest == null)
                {
                    response.DefaultNotFound();
                    return response;
                }

                existedRequest.IsDeleted = true;
                existedRequest.UpdatedBy = _loggedInUser.UserId;

                await _requestRepository.UpdateAsync(existedRequest);

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
