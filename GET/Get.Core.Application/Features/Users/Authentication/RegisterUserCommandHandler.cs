using AutoMapper;
using FluentValidation.Results;
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Application.Contracts;
using GET.Core.Application.Features.ServiceRequests;
using GET.Core.Application.Models;
using GET.Core.Application.Models.Authentication;
using GET.Core.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.Users.Authentication
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ReturnResult<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IHasher _hasher;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(IMapper mapper, IUserRepository userRepository, IServiceRepository serviceRepository,
            IHasher hasher, ILogger<RegisterUserCommandHandler> logger)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
            _hasher = hasher;
            _logger = logger;
        }
        public async Task<ReturnResult<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<bool>();
            try
            {
                var validator = new RegisterUserCommandValidator(_userRepository, _serviceRepository);
                var validationResult = validator.Validate(request);

                if (validationResult.Errors.Count > 0)
                {
                    response.BadRequest(validationResult.Errors.Select(r => r.ErrorMessage).ToList());
                    return response;
                }

                await RegisterUser(request);

                response.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                response.DefaultServerError();
            }
            return response;
        }

        private async Task<User> RegisterUser(RegisterUserCommand request)
        {
            var user = _mapper.Map<User>(request);

            HashUserPassword(user, request.Password);

            user.ServiceRequests = new List<ServiceRequest>() { new ServiceRequest { ServiceId = request.ServiceId, StatusId = (int)ServiceRequestStatus.InProgress, CreatedBy = user.Id.ToString() } };
            await _userRepository.AddAsync(user);
            return user;
        }
        private void HashUserPassword(User user, string password)
        {
            user.PasswordHash = _hasher.Hash(password);
        }
    }
}
