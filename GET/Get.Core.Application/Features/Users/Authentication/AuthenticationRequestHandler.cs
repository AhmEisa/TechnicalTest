using AutoMapper;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Application.Contracts;
using GET.Core.Application.Models;
using GET.Core.Application.Models.Authentication;
using GET.Core.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.Users.Authentication
{
    public class AuthenticationRequestHandler : IRequestHandler<AuthenticationRequest, ReturnResult<AuthenticationResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;
        private readonly ILogger<AuthenticationRequestHandler> _logger;

        public AuthenticationRequestHandler(IUserRepository userRepository, IHasher hasher, ILogger<AuthenticationRequestHandler> logger)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _logger = logger;
        }
        public async Task<ReturnResult<AuthenticationResponse>> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<AuthenticationResponse>();
            try
            {
                var validator = new AuthenticationRequestValidator(_userRepository);
                var validationResult = validator.Validate(request);

                if (validationResult.Errors.Count > 0)
                {
                    response.BadRequest(validationResult.Errors.Select(r => r.ErrorMessage).ToList());
                    return response;
                }

                var user = await _userRepository.FindByEmailAsync(request.Email);
                var isPasswordMatched = _hasher.Verify(request.Password, user.PasswordHash);
                if (!isPasswordMatched)
                {
                    response.BadRequest("Invalid email or password");
                    return response;
                }

                var userInfo = _userRepository.AuthenticateAsync(user);
                response.Success(userInfo);

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
