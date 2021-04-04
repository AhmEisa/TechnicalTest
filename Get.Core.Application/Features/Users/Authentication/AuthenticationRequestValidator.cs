using FluentValidation;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Application.Models.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.Users.Authentication
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationRequestValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(p => p.Email).NotEmpty().WithMessage("{PropertyName} is required")
                                 .MustAsync(MustBeExist).WithMessage("Invalid email or password");

            RuleFor(p => p.Password).NotEmpty().WithMessage("{PropertyName} is required");
        }

        private async Task<bool> MustBeExist(string email, CancellationToken token)
        {
            return (await _userRepository.FindByEmailAsync(email)) != null;
        }
    }
}
