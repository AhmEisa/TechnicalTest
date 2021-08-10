using FluentValidation;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.ServiceRequests
{
    public class CreateServiceRequestCommandValidator : AbstractValidator<CreateServiceRequestCommand>
    {
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAsyncRepository<Service> _serviceRepository;

        public CreateServiceRequestCommandValidator(IAsyncRepository<User> userRepository, IAsyncRepository<Service> serviceRepository)
        {
            RuleFor(p => p.UserId).NotEmpty().WithMessage("{PropertyName} is required")
                                  .MustAsync(UserExists).WithMessage("{PropertyName} is invalid");

            RuleFor(p => p.ServiceId).NotEmpty().WithMessage("{PropertyName} is required")
                                     .MustAsync(ServiceExists).WithMessage("{PropertyName} is invalid");

            RuleFor(p => p.StatusId).NotEmpty().WithMessage("{PropertyName} is required")
                 .Must(WithinStatusValuesRange).WithMessage("{PropertyName} is invalid");

            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
        }

        private async Task<bool> UserExists(Guid userId, CancellationToken token)
        {
            return (await _userRepository.GetByIdAsync(userId)) != null;
        }

        private async Task<bool> ServiceExists(Guid serviceId, CancellationToken token)
        {
            return (await _serviceRepository.GetByIdAsync(serviceId)) != null;
        }

        private bool WithinStatusValuesRange(int statusId)
        {
            return Enum.IsDefined(typeof(ServiceRequestStatus), statusId);
        }
    }
}
