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
using System.Threading;
using System.Threading.Tasks;

namespace GET.Core.Application.Features.Lookups
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ReturnResult<List<UserVm>>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<User> _userRepository;
        private readonly ILogger<GetServiceListQueryHandler> _logger;

        public GetUserListQueryHandler(IMapper mapper, IAsyncRepository<User> userRepository, ILoggedInUserService loggedInUser, ILogger<GetServiceListQueryHandler> logger)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<ReturnResult<List<UserVm>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var response = new ReturnResult<List<UserVm>>();
            try
            {
                var allUsers = await _userRepository.ListAllAsync();
                if (allUsers == null || allUsers.Count == 0)
                {
                    response.DefaultNotFound();
                    return response;
                }

                response.Success(_mapper.Map<List<UserVm>>(allUsers));
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
