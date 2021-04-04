using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Domain;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class UpdateServiceRequestStatusCommandValidator : AbstractValidator<UpdateServiceRequestStatusCommand>
    {
        private readonly IAsyncRepository<ServiceRequest> _requestRepository;

        public UpdateServiceRequestStatusCommandValidator(IAsyncRepository<ServiceRequest> requestRepository)
        {
            _requestRepository = requestRepository;

            RuleFor(p => p.RequestId).NotEmpty().WithMessage("{PropertyName} is required")
                                     .MustAsync(RequestExists).WithMessage("{PropertyName} is invalid");

            RuleFor(p => p.StatusId).NotEmpty().WithMessage("{PropertyName} is required")
                .Must(WithinStatusValuesRange).WithMessage("{PropertyName} is invalid");
        }

        private async Task<bool> RequestExists(Guid requestId, CancellationToken token)
        {
            return (await _requestRepository.GetByIdAsync(requestId)) != null;
        }

        private bool WithinStatusValuesRange(int statusId)
        {
            return Enum.IsDefined(typeof(ServiceRequestStatus), statusId);
        }
    }
}
