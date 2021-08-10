using GET.Core.Application.Models;
using GET.Core.Application.Models.Authentication;
using MediatR;
using System.Collections.Generic;

namespace GET.Core.Application.Features.Lookups
{
    public class GetUserListQuery : IRequest<ReturnResult<List<UserVm>>>
    {
    }
}
