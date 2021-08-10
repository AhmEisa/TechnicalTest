using FluentValidation;
using Get.Core.Application.Contracts.Persistence;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.Users.Authentication
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceRepository _serviceRepository;

        public RegisterUserCommandValidator(IUserRepository userRepository, IServiceRepository serviceRepository)
        {
            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.LastName).NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.Email).NotEmpty().WithMessage("{PropertyName} is required")
                                 .Must(ValidEmailFormat).WithMessage("{PropertyName} is invalid format.")
                                 .MustAsync(EmailMustBeUnique).WithMessage("An Email with the same value already exists");

            RuleFor(p => p.Password).NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.ServiceId).NotEmpty().WithMessage("{PropertyName} is required")
                                     .MustAsync(MustBeExist).WithMessage("{PropertyName} is not Exists");
        }

        private async Task<bool> MustBeExist(Guid serviceId, CancellationToken token)
        {
            return await _serviceRepository.GetByIdAsync(serviceId) != null;
        }
        private bool ValidEmailFormat(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }
        private async Task<bool> EmailMustBeUnique(string email, CancellationToken token)
        {
            return (await _userRepository.FindByEmailAsync(email)) == null;
        }
    }
}
